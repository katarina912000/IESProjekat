using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class RegularIntervalSchedule :BasicIntervalSchedule
    {
        private DateTime endTime;
        private List<long> timePoints;

        public RegularIntervalSchedule(long globalId) : base(globalId)
        {
        }

        public DateTime EndTime { get => endTime; set => endTime = value; }
        public List<long> TimePoints { get => timePoints; set => timePoints = value; }

        public override bool Equals(object obj)
        {
            var schedule = obj as RegularIntervalSchedule;
            return schedule != null &&
                   base.Equals(obj) &&
                   EndTime == schedule.EndTime &&
                   EqualityComparer<List<long>>.Default.Equals(TimePoints, schedule.TimePoints);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
                case ModelCode.REGULARINTERVALSCHEDULE_TIMEPOINTS:
                

                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
                    property.SetValue(endTime);
                    break;

                case ModelCode.REGULARINTERVALSCHEDULE_TIMEPOINTS:
                    property.SetValue(timePoints);
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
                case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
                    endTime = property.AsDateTime();
                    break;

                case ModelCode.REGULARINTERVALSCHEDULE_TIMEPOINTS:
                    timePoints = property.AsReferences();
                    break;

                

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return timePoints.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (timePoints != null && timePoints.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULARINTERVALSCHEDULE_TIMEPOINTS] = timePoints.GetRange(0, timePoints.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE:
                    timePoints.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE:

                    if (timePoints.Contains(globalId))
                    {
                        timePoints.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion IReference implementation	
    }
}
