using StressedBread.Flashcards.Data;
using StressedBread.Flashcards.Data.Queries;
using StressedBread.Flashcards.Models;
using StressedBread.Flashcards.UI;
using static StressedBread.Flashcards.Enums;

namespace StressedBread.Flashcards.Controllers;
internal class ReportsController
{
    private readonly ReportsMenu _reportsMenu;
    private readonly DatabaseAccess _databaseAccess;
    private readonly ReportQueries _reportQueries;
    private readonly StacksQueries _stacksQueries;

    private List<StacksModel> _stacks = new List<StacksModel>();
    internal ReportsController(ReportsMenu reportsMenu, DatabaseAccess databaseAccess, ReportQueries reportQueries, StacksQueries stacksQueries)
    {
        _reportsMenu = reportsMenu;
        _databaseAccess = databaseAccess;
        _reportQueries = reportQueries;
        _stacksQueries = stacksQueries;
    }

    internal List<StacksModel> GetAllStacks()
    {
        return _databaseAccess.Reader<StacksModel>(_stacksQueries.GetAllStacksQuery());
    }

    internal void ViewReports()
    {
        while (true)
        {
            var selection = _reportsMenu.ReportsMainMenuView();

            switch (selection)
            {
                case ReportMenuOption.SessionsPerMonthPerStack:
                    SessionsPerMonthPerStackReport();
                    break;
                case ReportMenuOption.AverageScorePerMonthPerStack:
                    AverageScorePerMonthPerStackReport();
                    break;
                case ReportMenuOption.BackToMainMenu:
                    return;
            }
        }
    }

    internal void SessionsPerMonthPerStackReport()
    {
        _stacks = GetAllStacks();
        string result = _reportsMenu.StacksViewReport(_stacks);

        if (String.Equals(result.Trim(), "0", StringComparison.OrdinalIgnoreCase))
            return;

        string year = _reportsMenu.YearToFilter();
        var reportModel = _databaseAccess.ReportQuery(_reportQueries.SessionsPerMonthPerStackQuery(), new { Year = year, StackName = result });
        
        if (reportModel == null)
        {
            _reportsMenu.InvalidReport();
            return;
        }

        _reportsMenu.ReportView(reportModel, year);
    }

    internal void AverageScorePerMonthPerStackReport()
    {
        _stacks = GetAllStacks();
        string result = _reportsMenu.StacksViewReport(_stacks);

        if (String.Equals(result.Trim(), "0", StringComparison.OrdinalIgnoreCase))
            return;

        string year = _reportsMenu.YearToFilter();
        var reportModel = _databaseAccess.ReportQuery(_reportQueries.AverageScorePerMonthPerStackQuery(), new { Year = year, StackName = result });

        if (reportModel == null)
        {
            _reportsMenu.InvalidReport();
            return;
        }

        _reportsMenu.ReportView(reportModel, year);
    }
}
