<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions"
xmlns:my="urn:my-namespace"
xmlns:msxsl="urn:schemas-microsoft-com:xslt"
xmlns:rim="urn:oasis:names:tc:ebxml-regrep:rim:xsd:3.0"
xmlns:rs="urn:oasis:names:tc:ebxml-regrep:registry:xsd:3.0" 
xmlns:tns="urn:oasis:names:tc:ebxml-regrep:xsd:lcm:3.0"
xmlns="urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0">

	<msxsl:script language="javascript" implements-prefix="my">
	<![CDATA[
		function S4() {
		   return (((1+Math.random())*0x10000)|0).toString(16).substring(1);
		}
		function guid() {
		   return (S4()+S4()+"-"+S4()+"-"+S4()+"-"+S4()+"-"+S4()+S4()+S4());
		}
		function UUID()
		{
			return ("urn:uuid:" + guid());
		}
	]]>
	</msxsl:script>
	
	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>

	<!-- Identity template for elements -->
    <xsl:template match="*">
		<xsl:element name="{local-name()}">
		  <xsl:apply-templates select="@*|node()"/>
		</xsl:element>
    </xsl:template>

	<!-- Identity template for attributes -->
    <xsl:template match="@*">
		<xsl:attribute name="{local-name()}">
		  <xsl:value-of select="."/>
		</xsl:attribute>
    </xsl:template>

    <!-- Different Namespaces -->
	<xsl:template match="*[local-name()='SubmitObjectsRequest']">
		<xsl:element name="tns:SubmitObjectsRequest">
			<xsl:apply-templates select="@*|node()"/>
		</xsl:element>
	</xsl:template>

	<!-- LeafRegistryObjectList becomes RegistryObjectList -->
	<xsl:template match="*[local-name()='LeafRegistryObjectList']">
		<xsl:element name="RegistryObjectList" xpath-default-namespace="urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0">
			<xsl:apply-templates select="@*|node()"/>
		</xsl:element>
	</xsl:template>

	<!-- Order of the elements changes -->
	<xsl:template match="*[local-name()='ExtrinsicObject' or local-name()='RegistryPackage']">
		<xsl:element name="{local-name()}">
			<xsl:apply-templates select="@*"/>
			<xsl:apply-templates select="./*[local-name()='Slot']" />
			<xsl:apply-templates select="./*[local-name()='Name']" />
			<xsl:apply-templates select="./*[local-name()='Description']" />
			<xsl:apply-templates select="./*[local-name()='Classification']" />
			<xsl:apply-templates select="./*[local-name()='ExternalIdentifier']" />
			<xsl:apply-templates select="./*[local-name()!='Slot' and local-name()!='Name' and local-name()!='Description' and local-name()!='Classification' and local-name()!='ExternalIdentifier']" />
		</xsl:element>
	</xsl:template>
	
	<xsl:template match="*[local-name()='Slot']">
		<xsl:if test="not(string(@name) = 'URI' and local-name(..) = 'ExtrinsicObject')">
			<xsl:element name="{local-name()}">
				<xsl:apply-templates  select="@*|node()"/>
			</xsl:element>
		</xsl:if>
	</xsl:template>
	
	<!-- id attribute is required for Classification and Association -->
	<xsl:template match="*[local-name()='Classification' or local-name()='Association']">
		<xsl:element name="{local-name()}">
			<xsl:if test="count(./@id) = 0">
				<xsl:attribute name="id">
					<xsl:value-of select="my:UUID()"/>
				</xsl:attribute>
			</xsl:if>
			<xsl:choose>
				<xsl:when test="local-name()='Classification'">
					<xsl:apply-templates select="@*"/>
					<xsl:apply-templates select="./*[local-name()='Slot']" />
					<xsl:apply-templates select="./*[local-name()='Name']" />
					<xsl:apply-templates select="./*[local-name()='Description']" />
					<xsl:apply-templates select="./*[local-name()='VersionInfo']" />			
					<xsl:apply-templates select="./*[local-name()='VersionInfo']" />						
					<xsl:apply-templates select="./*[local-name()='Classification']" />
					<xsl:apply-templates select="./*[local-name()='ExternalIdentifier']" />				
				</xsl:when>
				<xsl:otherwise>
					<xsl:apply-templates select="@*|node()"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:element>
	</xsl:template>
	
	<!-- id attribute is required for ExternalIdentifier, registryObject is required for ExternalIdentifier -->
	<xsl:template match="*[local-name()='ExternalIdentifier']">
		<xsl:element name="{local-name()}">
			<xsl:if test="count(./@id) = 0">
				<xsl:attribute name="id">
					<xsl:value-of select="my:UUID()"/>
				</xsl:attribute>
			</xsl:if>
			<xsl:if test="count(./@registryObject) = 0">
				<xsl:attribute name="registryObject"><xsl:value-of select="../@id"/></xsl:attribute>
			</xsl:if>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:element>
	</xsl:template>

	<!-- Status attribute value format changes from Approved
		 to urn:oasis:names:tc:ebxml-regrep:StatusType:Approved 
	-->
	<xsl:template match="@status">
		<xsl:choose>
			<xsl:when test="starts-with(.,'urn:')">
				<xsl:copy />
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="status">
					<xsl:value-of select="concat('urn:oasis:names:tc:ebxml-regrep:StatusType:',.)" />
				</xsl:attribute>			
			</xsl:otherwise>
		</xsl:choose>		
	</xsl:template>
	
	<!-- ObjectType attribute changes format, changing from a text name to a UUID -->
	<xsl:template match="@objectType">
		<xsl:choose>
			<xsl:when test="starts-with(.,'urn:')">
				<xsl:copy />
			</xsl:when>
			<xsl:otherwise>
				<xsl:attribute name="objectType">
					<xsl:value-of select="concat('urn:oasis:names:tc:ebxml-regrep:ObjectType:RegistryObject:',.)" />
				</xsl:attribute>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
</xsl:stylesheet>
