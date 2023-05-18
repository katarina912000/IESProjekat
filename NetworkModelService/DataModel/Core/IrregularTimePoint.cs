using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class IrregularTimePoint : IdentifiedObject
    {
        private float value1;
        private float value2;
        private long intervalSchedule;

        public IrregularTimePoint(long globalId) : base(globalId)
        {
        }

        public float Value1 { get => value1; set => value1 = value; }
        public float Value2 { get => value2; set => value2 = value; }
        public long IntervalSchedule { get => intervalSchedule; set => intervalSchedule = value; }

        public override bool Equals(object obj)
        {
            var point = obj as IrregularTimePoint;
            return point != null &&
                   base.Equals(obj) &&
                   value1 == point.value1 &&
                   value2 == point.value2 &&
                   intervalSchedule == point.intervalSchedule;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.IRREGULARTIMEPOINT_VALUE1:
                case ModelCode.IRREGULARTIMEPOINT_VALUE2:
                case ModelCode.IRREGULARTIMEPOINT_INTERVALSCHEDULE:

                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
              
                case ModelCode.IRREGULARTIMEPOINT_VALUE1:
                    property.SetValue(value1);
                    break;

                case ModelCode.IRREGULARTIMEPOINT_VALUE2:
                    property.SetValue(value2);
                    break;

                case ModelCode.IRREGULARTIMEPOINT_INTERVALSCHEDULE:
                    property.SetValue(intervalSchedule);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                
                case ModelCode.IRREGULARTIMEPOINT_VALUE1:
                    value1 = property.AsFloat();
                    break;

                case ModelCode.IRREGULARTIMEPOINT_VALUE2:
                    value2 = property.AsFloat();
                    break;

                case ModelCode.IRREGULARTIMEPOINT_INTERVALSCHEDULE:
                    intervalSchedule = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation	

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (intervalSchedule != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.IRREGULARTIMEPOINT_INTERVALSCHEDULE] = new List<long>();
                references[ModelCode.IRREGULARTIMEPOINT_INTERVALSCHEDULE].Add(intervalSchedule);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
