using System;

namespace FTN.Common
{	
	
	public enum UnitSymbol : short
	{
			A,
            F,
            H,
            Hz,
            J,
            N,
            Pa,
            S,
            V,
            VA,
            VAh,
            VAr,
            VArh,
            W,
            Wh,
            deg,
            degC,
            g,
            h,
            m,
            m2,
            m3,
            min,
            none,
            ohm,
            rad,
            s

    }
    public enum UnitMultiplier : short
    {
        G,
        M,
        T,
        c,
        d,
        k,
        m,
        micro,
        n,
        none,
        p


    }
    public enum CurveStyle : short
    {
        constantYValue,
        formula,
        rampYValue,
        straightLineYValues
    }

}
