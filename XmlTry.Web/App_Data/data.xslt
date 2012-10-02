<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
version="1.0">
  <xsl:template match="/">
    <html>
      <xsl:for-each select="questions/openedQuestion">
        <form onsubmit="return false;">
          <input type="hidden" name="identity">
            <xsl:attribute name="value">
              <xsl:value-of select="@identity" />
            </xsl:attribute>
          </input>
          <p>
            <xsl:value-of select="text"/>
          </p>
          <p>
            <xsl:for-each select="/variants/variant">
              <input type="radio" name="variant">
                <xsl:attribute name="value">
                  <xsl:value-of select="/" />
                </xsl:attribute>
              </input>
            </xsl:for-each>
          </p>
          <input type="button" value="Answer" />
        </form>
      </xsl:for-each>
    </html>
  </xsl:template>
</xsl:stylesheet>