using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using NSubstitute;

namespace BaseClassCoupling
{
    public interface ILOG
    {
        void Lllog(string message);
    }

    public interface ISss
    {
        decimal Salary(int id);
    }

    public static class DebugHelper
    {
        public static void Info(string message)
        {
            //you can't modified this function
            throw new NotImplementedException();
        }
    }

    public static class SalaryRepo
    {
        internal static decimal Get(int id)
        {
            //you can't modified this function
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class BaseClassCouplingTests
    {
        [TestMethod]
        public void calculate_half_year_employee_bonus()
        {
            //if my monthly salary is 1200, working year is 0.5, my bonus should be 600
            var lessThanOneYearEmployee = new LessThanOneYearEmployee()
            {
                Id = 91,
                Today = new DateTime(2018, 1, 27),
                StartWorkingDate = new DateTime(2017, 7, 29)
            };

            var fakeLog = Substitute.For<Log>();
            fakeLog.When(x => x.Lllog(Arg.Any<string>())).Do(x => { });
            lessThanOneYearEmployee.Log = fakeLog;
            var fakeSss = Substitute.For<Sss>();
            fakeSss.Salary(Arg.Any<int>()).ReturnsForAnyArgs(1200);
            lessThanOneYearEmployee.Sss = fakeSss;

            var actual = lessThanOneYearEmployee.GetYearlyBonus();

            Assert.AreEqual(600, actual);
        }
    }

    public abstract class Employee
    {
        private ILOG _log;
        private ISss _sss;

        public int Id { get; set; }

        public ILOG Log
        {
            get => _log ?? new Log();
            set => _log = value;
        }

        public ISss Sss
        {
            get => _sss ?? new Sss();
            set => _sss = value;
        }

        public DateTime StartWorkingDate { get; set; }
        public DateTime Today { get; set; }

        public abstract decimal GetYearlyBonus();

        protected decimal GetMonthlySalary()
        {
            Log.Lllog($"query monthly salary id:{Id}");
            return _sss.Salary(this.Id);
        }
    }

    public class LessThanOneYearEmployee : Employee
    {
        public override decimal GetYearlyBonus()
        {
            Log.Lllog("--get yearly bonus--");
            var salary = this.GetMonthlySalary();
            Log.Lllog($"id:{Id}, his monthly salary is:{salary}");
            return Convert.ToDecimal(this.WorkingYear()) * salary;
        }

        private double WorkingYear()
        {
            Log.Lllog("--get working year--");
            var year = (Today - StartWorkingDate).TotalDays / 365;
            return year > 1 ? 1 : Math.Round(year, 2);
        }
    }

    public class Log : ILOG
    {
        public virtual void Lllog(string message)
        {
            DebugHelper.Info(message);
        }
    }

    public class Sss : ISss
    {
        public virtual decimal Salary(int id)
        {
            return SalaryRepo.Get(id);
        }
    }
}