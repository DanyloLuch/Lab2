<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<html>
			<body>
				<h2>Список студентів та викладачів</h2>
				<table border="1">
					<tr bgcolor="#9acd32">
						<th>ID</th>
						<th>Ім'я</th>
						<th>Факультет</th>
						<th>Кафедра</th>
						<th>Освітній рівень</th>
						<th>Освітній заклад</th>
						<th>Дата початку</th>
						<th>Предмети</th>
					</tr>
					<xsl:for-each select="StudentsAndStaff/*">
						<tr>
							<td>
								<xsl:value-of select="@id"/>
							</td>
							<td>
								<xsl:value-of select="@fullName"/>
							</td>
							<td>
								<xsl:value-of select="@faculty"/>
							</td>
							<td>
								<xsl:value-of select="@department"/>
							</td>
							<td>
								<xsl:value-of select="@educationLevel"/>
							</td>
							<td>
								<xsl:value-of select="@institution"/>
							</td>
							<td>
								<xsl:value-of select="@startDate"/>
							</td>
							<td>
								<xsl:for-each select="Subjects/Subject">
									<xsl:value-of select="@name"/>
									<xsl:text> </xsl:text>
								</xsl:for-each>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
