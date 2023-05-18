using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class CurveData :IdentifiedObject
    {
        private float xvalue;
        private float y1value;
        private float y2value;
        private float y3value;
        private long curve;

        public CurveData(long globalId) : base(globalId) { }

        public float Xvalue { get => xvalue; set => xvalue = value; }
        public float Y1value { get => y1value; set => y1value = value; }
        public float Y2value { get => y2value; set => y2value = value; }
        public float Y3value { get => y3value; set => y3value = value; }
        public long Curve { get => curve; set => curve = value; }

        public override bool Equals(object obj)
        {
            var data = obj as CurveData;
            return data != null &&
                   base.Equals(obj) &&
                   xvalue == data.xvalue &&
                   y1value == data.y1value &&
                   y2value == data.y2value &&
                   y3value == data.y3value &&
                   curve == data.curve;
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
                case ModelCode.CURVEDATA_XVALUE:
                case ModelCode.CURVEDATA_Y1VALUE:
                case ModelCode.CURVEDATA_Y2VALUE:
                case ModelCode.CURVEDATA_Y3VALUE:
                case ModelCode.CURVEDATA_CURVE:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CURVEDATA_XVALUE:
                    property.SetValue(xvalue);
                    break;

                case ModelCode.CURVEDATA_Y1VALUE:
                    property.SetValue(y1value);
                    break;

                case ModelCode.CURVEDATA_Y2VALUE:
                    property.SetValue(y2value);
                    break;

                case ModelCode.CURVEDATA_Y3VALUE:
                    property.SetValue(y3value);
                    break;
                case ModelCode.CURVEDATA_CURVE:
                    property.SetValue(curve);
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
                case ModelCode.CURVEDATA_XVALUE:
                    xvalue = property.AsFloat();
                    break;

                case ModelCode.CURVEDATA_Y1VALUE:
                    y1value = property.AsFloat(); break;

                case ModelCode.CURVEDATA_Y2VALUE:
                    y2value = property.AsFloat(); break;

                case ModelCode.CURVEDATA_Y3VALUE:
                    y3value = property.AsFloat(); break;
                case ModelCode.CURVEDATA_CURVE:
                    curve = property.AsReference(); break;



                default:
                    base.SetProperty(property);
                    break;
            }
        }

       

        #endregion IAccess implementation
        #region IReference implementation	

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (curve != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.CURVEDATA_CURVE] = new List<long>();
                references[ModelCode.CURVEDATA_CURVE].Add(curve);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation

    }
}
