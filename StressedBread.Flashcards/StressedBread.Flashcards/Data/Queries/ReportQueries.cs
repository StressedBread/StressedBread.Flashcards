namespace StressedBread.Flashcards.Data.Queries;
internal class ReportQueries
{
    internal string SessionsPerMonthPerStackQuery()
    {
        return @"SELECT 
	                [Stack Name],
                    ISNULL([January], 0) AS [January],
                    ISNULL([February], 0) AS [February],
                    ISNULL([March], 0) AS [March],
                    ISNULL([April], 0) AS [April],
                    ISNULL([May], 0) AS [May],
                    ISNULL([June], 0) AS [June],
                    ISNULL([July], 0) AS [July],
                    ISNULL([August], 0) AS [August],
                    ISNULL([September], 0) AS [September],
                    ISNULL([October], 0) AS [October],
                    ISNULL([November], 0) AS [November],
                    ISNULL([December], 0) AS [December]
                FROM
                (
	                SELECT 
		                stack.Name as [Stack Name],
		                study.Id AS [Session Id],
		                DATENAME(MONTH, study.SessionDate) AS [Month]
	                FROM dbo.StudySessions study
	                JOIN dbo.Stacks stack ON study.StackId = stack.Id
	                WHERE 
                        study.SessionDate >= DATEFROMPARTS(@Year, 1, 1) AND 
                        study.SessionDate < DATEFROMPARTS(@Year + 1, 1, 1) AND
                        stack.Name = @StackName
                ) AS SourceTable
                PIVOT
                (
	                COUNT([Session Id])
	                FOR [Month] IN ([January], [February], [March], [April], [May], [June], [July], [August], [September], [October], [November], [December])
                ) AS PivotTable;";
    }

    internal string AverageScorePerMonthPerStackQuery()
    {
        return @"SELECT 
	                [Stack Name],
                    ISNULL([January], 0) AS [January],
                    ISNULL([February], 0) AS [February],
                    ISNULL([March], 0) AS [March],
                    ISNULL([April], 0) AS [April],
                    ISNULL([May], 0) AS [May],
                    ISNULL([June], 0) AS [June],
                    ISNULL([July], 0) AS [July],
                    ISNULL([August], 0) AS [August],
                    ISNULL([September], 0) AS [September],
                    ISNULL([October], 0) AS [October],
                    ISNULL([November], 0) AS [November],
                    ISNULL([December], 0) AS [December]
                FROM
                (
	                SELECT 
		                stack.Name as [Stack Name],
		                study.Score AS [Score],
		                DATENAME(MONTH, study.SessionDate) AS [Month]
	                FROM dbo.StudySessions study
	                JOIN dbo.Stacks stack ON study.StackId = stack.Id
	                WHERE 
                        study.SessionDate >= DATEFROMPARTS(@Year, 1, 1) AND 
                        study.SessionDate < DATEFROMPARTS(@Year + 1, 1, 1) AND
                        stack.Name = @StackName
                ) AS SourceTable
                PIVOT
                (
	                AVG([Score])
	                FOR [Month] IN ([January], [February], [March], [April], [May], [June], [July], [August], [September], [October], [November], [December])
                ) AS PivotTable;";
    }
}
