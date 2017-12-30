<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/">
		<form action="#" method="post">
			<xsl:for-each select="questions/question">
				<input type="hidden" name="identity">
					<xsl:attribute name="value">
						<xsl:value-of select="@identity" />
					</xsl:attribute>
				</input>
				<p>
					<xsl:value-of select="text" />
				</p>
				<xsl:choose>
					<xsl:when test="variants">
						<p>
							<xsl:for-each select="variants/variant">
								<xsl:variable name="inputType">
									<xsl:choose>
										<xsl:when test="count(../variant[@answer]) = 1">radio</xsl:when>
										<xsl:otherwise>checkbox</xsl:otherwise>
									</xsl:choose>
								</xsl:variable>
								<input name="variant">
									<xsl:attribute name="value">
										<xsl:value-of select="." />
									</xsl:attribute>
									<xsl:attribute name="type">
										<xsl:value-of select="$inputType" />
									</xsl:attribute>
								</input>
								<xsl:value-of select="." />
								<br />
							</xsl:for-each>
						</p>
					</xsl:when>
					<xsl:otherwise>
						<p>
							<input type="text" name="answerText" />
						</p>
					</xsl:otherwise>
				</xsl:choose>
				<br />
			</xsl:for-each>
			<input type="button" value="Answer" />
		</form>
	</xsl:template>
</xsl:stylesheet>