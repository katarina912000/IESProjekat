﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using FTN.Common;



namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class PowerSystemResource : IdentifiedObject
    {
        private List<long> outageSchedules;

        public List<long> OutageSchedules { get => outageSchedules; set => outageSchedules = value; }

        public PowerSystemResource(long globalId)
            : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            var resource = obj as PowerSystemResource;
            return resource != null &&
                   base.Equals(obj) &&
                   EqualityComparer<List<long>>.Default.Equals(outageSchedules, resource.outageSchedules);
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
                case ModelCode.PSR_OUTAGESCHEDULES:
               
                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PSR_OUTAGESCHEDULES:
                    property.SetValue(outageSchedules);
                    break;

              

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
           
                    base.SetProperty(property);
                 
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return outageSchedules.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (outageSchedules != null && outageSchedules.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.PSR_OUTAGESCHEDULES] = outageSchedules.GetRange(0, outageSchedules.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE:
                    outageSchedules.Add(globalId);
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
                case ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE:

                    if (outageSchedules.Contains(globalId))
                    {
                        outageSchedules.Remove(globalId);
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

