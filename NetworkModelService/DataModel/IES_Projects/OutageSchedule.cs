using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class OutageSchedule : IrregularIntervalSchedule
    {
        private long powerSystemResource;

        public OutageSchedule(long globalId) : base(globalId)
        {
        }

        public long PowerSystemResource { get => powerSystemResource; set => powerSystemResource = value; }

        public override bool Equals(object obj)
        {
            var schedule = obj as OutageSchedule;
            return schedule != null &&
                   base.Equals(obj) &&
                   PowerSystemResource == schedule.PowerSystemResource;
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
                case ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE:

               

                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property property)
        {

            switch (property.Id)
            {
                case ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE:
                    property.SetValue(powerSystemResource);
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
                case ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE:
                    powerSystemResource = property.AsReference();
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
            if (powerSystemResource != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE] = new List<long>();
                references[ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE].Add(powerSystemResource);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
