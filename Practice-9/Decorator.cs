using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_9
{
    public interface IReport
    {
        string Generate();
    }
    public class SalesReport : IReport
    {
        public string Generate()
        {
            return "Sales report: \n Tea: 2000tg \n Latte: 9000tg";
        }
    }
    public class UserReport : IReport
    {
        public string Generate()
        {
            return "User report: \n Client 1: 2 Tea \n Client 2: 6 Latte";
        }
    }

    public abstract class ReportDecorator : IReport
    {
        protected IReport _report;
        protected ReportDecorator(IReport report)
        {
            _report = report;
        }
        public abstract string Generate();
    }
    public class DateFilterDecorator : ReportDecorator
    {
        private DateTime _startDate;
        private DateTime _endDate;

        public DateFilterDecorator(IReport report, DateTime startDate, DateTime endDate)
            : base(report)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public override string Generate()
        {
            return $"Filtered by DateTime: \n {_startDate} - {_endDate} \n" +_report.Generate();
        }
    }
    public class SortingDecorator : ReportDecorator
    {
        private string _sortBy;

        public SortingDecorator(IReport report, string sortBy) : base(report)
        {
            _sortBy = sortBy;
        }

        public override string Generate()
        {
            return $"Sorted by: {_sortBy}:\n" + _report.Generate();
        }
    }
    public class CsvExportDecorator : ReportDecorator
    {
        public CsvExportDecorator(IReport report) : base(report) { }

        public override string Generate()
        {
            return $"Exported as CSV:\n" + _report.Generate();
        }
    }
    public class PdfExportDecorator : ReportDecorator
    {
        public PdfExportDecorator(IReport report) : base(report) { }

        public override string Generate()
        {
            return $"Exported as PDF:\n" + _report.Generate();
        }
    }
    public class SalesAmountFilterDecorator : ReportDecorator
    {
        private decimal _minAmount;
        private decimal _maxAmount;

        public SalesAmountFilterDecorator(IReport report, decimal minAmount, decimal maxAmount)
            : base(report)
        {
            _minAmount = minAmount;
            _maxAmount = maxAmount;
        }

        public override string Generate()
        {
            return $"Filtered by amount:\n {_minAmount} - {_minAmount}:\n" + _report.Generate();
        }
    }
    internal class Decorator
    {
    }
}
