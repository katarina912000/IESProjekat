using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),

		PROTECTEDSWITCH                     = 0x0001,
		REGULARINTERVALSCHEDULE = 0x0002,
        CURVE = 0x0003,
        OUTAGESCHEDULE = 0x0004,
        IRREGULARTIMEPOINT = 0x0005,
        REGULARTIMEPOINT = 0x0006,                            
        CURVEDATA = 0x0007,
       
    }

    [Flags]
    public enum ModelCode : long
    {
        IDOBJ                               = 0x1000000000000000,
        IDOBJ_GID                           = 0x1000000000000104,
        IDOBJ_ALIASNAME                     = 0x1000000000000207,
        IDOBJ_MRID                          = 0x1000000000000307,
        IDOBJ_NAME                          = 0x1000000000000407,

        PSR                                 = 0x1100000000000000,
        PSR_OUTAGESCHEDULES                 = 0X1100000000000119,
            
        

        REGULARTIMEPOINT                    = 0x1200000000060000,
        REGULARTIMEPOINT_SEQUENCENUMBER     = 0x1200000000060103,
        REGULARTIMEPOINT_VALUE1             = 0x1200000000060205,
        REGULARTIMEPOINT_VALUE2             = 0x1200000000060305,
        REGULARTIMEPOINT_INTERVALSCHEDULE   = 0x1200000000060409,

        CURVEDATA                           = 0x1300000000070000,
        CURVEDATA_XVALUE                    = 0x1300000000070105,
        CURVEDATA_Y1VALUE                   = 0x1300000000070205,
        CURVEDATA_Y2VALUE                   = 0x1300000000070305,
        CURVEDATA_Y3VALUE                   = 0x1300000000070405,
        CURVEDATA_CURVE                     = 0x1300000000070509,

        CURVE                               = 0x1400000000030000,
        CURVE_XMULTIPLIER                   = 0x140000000003010a,
        CURVE_XUNIT                         = 0x140000000003020a,
        CURVE_Y1MULTIPLIER                  = 0x140000000003030a,
        CURVE_Y1UNIT                        = 0x140000000003040a,
        CURVE_Y2MULTIPLIER                  = 0x140000000003050a,
        CURVE_Y2UNIT                        = 0x140000000003060a,
        CURVE_Y3MULTIPLIER                  = 0x140000000003070a,
        CURVE_Y3UNIT                        = 0x140000000003080a,
        CURVE_CURVEDATAS                    = 0x1400000000030919,
        CURVE_CURVESTYLE                    = 0x140000000003100a,


        BASICINTERVALSCHEDULE                    = 0x1500000000000000,
        BASICINTERVALSCHEDULE_STARTTIME          = 0x1500000000000108,
        BASICINTERVALSCHEDULE_VALUE1MULTIPLIER   = 0x150000000000020a,
        BASICINTERVALSCHEDULE_VALUE1UNIT         = 0x150000000000030a,
        BASICINTERVALSCHEDULE_VALUE2MULTIPLIER   = 0x150000000000040a,
        BASICINTERVALSCHEDULE_VALUE2UNIT         = 0x150000000000050a,


        IRREGULARTIMEPOINT                      = 0x1600000000050000,
        IRREGULARTIMEPOINT_VALUE1               = 0x1600000000050105,
        IRREGULARTIMEPOINT_VALUE2               = 0x1600000000050205,
        IRREGULARTIMEPOINT_INTERVALSCHEDULE     = 0x1600000000050309,


        EQUIPMENT                               = 0x1110000000000000,
        EQUIPMENT_AGGREGATE                     = 0x1110000000000101,
        EQUIPMENT_NORMALLYINSERVICE             = 0x1110000000000201,


        CONDUCTINGEQUIPMENT                     = 0x1111000000000000,

        SWITCH                                  = 0x1111100000000000,
        SWITCH_NORMALOPEN                       = 0x1111100000000101,
        SWITCH_RETAINED                         = 0x1111100000000201,
        SWITCH_SWITCHONCOUNT                    = 0x1111100000000303,
        SWITCH_SWITCHONDATE                     = 0x1111100000000408,


        PROTECTEDSWITCH                         = 0x1111110000010000,
        
        REGULARINTERVALSCHEDULE                 = 0x1510000000020000,
        REGULARINTERVALSCHEDULE_ENDTIME         = 0x1510000000020108,
        REGULARINTERVALSCHEDULE_TIMEPOINTS      = 0x1510000000020219,

        IRREGULARINTERVALSCHEDULE               = 0x1520000000000000,
        IRREGULARINTERVALSCHEDULE_TIMEPOINTS    = 0x1520000000000119,

        OUTAGESCHEDULE                          = 0x1521000000040000,
        OUTAGESCHEDULE_POWERSYSTEMRESOURCE      = 0x1521000000040109,

    }

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}


