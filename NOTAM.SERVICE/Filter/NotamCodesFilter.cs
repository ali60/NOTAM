using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NOTAM.SERVICE
{
    public class NotamCodesFilter
    {

        private string _subjectFilter;
        public string SubjectFilter
        {
            get { return _subjectFilter; }
            set { _subjectFilter = value; }
        }


        private string _subjectdescFilter;
        public string SubjectDescFilter
        {
            get { return _subjectdescFilter; }
            set { _subjectdescFilter = value; }
        }

        private string _conditionFilter;
        public string ConditionFilter
        {
            get { return _conditionFilter; }
            set { _conditionFilter = value; }
        }

        private string _conditiondescFilter;
        public string ConditionDescFilter
        {
            get { return _conditiondescFilter; }
            set
            {
                _conditiondescFilter = value;
            }
        }



    }
}
