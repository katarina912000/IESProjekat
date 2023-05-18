using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            //// import all concrete model types (DMSType enum)
            //ImportBaseVoltages();
            //ImportLocations();
            //ImportPowerTransformers();
            //ImportTransformerWindings();
            //ImportWindingTests();
            ImportProtectedSwitch();
            ImportRegularIntervalSchedule();
            ImportCurve();
            ImportOutageSchedule();
            ImportIrregularTimePoint();
            ImportRegularTimePoint();
            ImportCurveData();


            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

        #region Import
        private void ImportCurveData()
        {
            SortedDictionary<string, object> cimCurveDatas = concreteModel.GetAllObjectsOfType("FTN.CurveData");
            if (cimCurveDatas != null)
            {
                foreach (KeyValuePair<string, object> cimCurveDataPair in cimCurveDatas)
                {
                    FTN.CurveData cimCurveData = cimCurveDataPair.Value as FTN.CurveData;

                    ResourceDescription rd = CreateCurveDataResourceDescription(cimCurveData);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("CurveData ID = ").Append(cimCurveData.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("CurveData ID = ").Append(cimCurveData.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateCurveDataResourceDescription(FTN.CurveData cimCurveData)
		{
			ResourceDescription rd = null;
			if (cimCurveData != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVEDATA, importHelper.CheckOutIndexForDMSType(DMSType.CURVEDATA));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimCurveData.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateCurveDataProperties(cimCurveData, rd,importHelper,report);
			}
			return rd;
		}

        private void ImportCurve()
        { 
            SortedDictionary<string, object> cimCurves = concreteModel.GetAllObjectsOfType("FTN.Curve");
            if (cimCurves != null)
            {
                foreach (KeyValuePair<string, object> cimCurvePair in cimCurves)
                {
                    FTN.Curve cimCurve = cimCurvePair.Value as FTN.Curve;

                    ResourceDescription rd = CreateCurveResourceDescription(cimCurve);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Curve ID = ").Append(cimCurve.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Curve ID = ").Append(cimCurve.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateCurveResourceDescription(FTN.Curve cimCurve)
		{
			ResourceDescription rd = null;
			if (cimCurve != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVE, importHelper.CheckOutIndexForDMSType(DMSType.CURVE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimCurve.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateCurveProperties(cimCurve, rd,importHelper,report);
			}
			return rd;
		}

        private void ImportRegularTimePoint()
        {
            SortedDictionary<string, object> cimRegularTimePoints = concreteModel.GetAllObjectsOfType("FTN.RegularTimePoint");
            if (cimRegularTimePoints != null)
            {
                foreach (KeyValuePair<string, object> cimRegularTimePointsPair in cimRegularTimePoints)
                {
                    FTN.RegularTimePoint cimRegularTimePoint = cimRegularTimePointsPair.Value as FTN.RegularTimePoint;

                    ResourceDescription rd = CreateRegularTimePointResourceDescription(cimRegularTimePoint);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegularTimePoint ID = ").Append(cimRegularTimePoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegularTimePoint ID = ").Append(cimRegularTimePoint.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateRegularTimePointResourceDescription(FTN.RegularTimePoint cimRegularTimePoint)
		{
			ResourceDescription rd = null;
			if (cimRegularTimePoint != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULARTIMEPOINT, importHelper.CheckOutIndexForDMSType(DMSType.REGULARTIMEPOINT));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimRegularTimePoint.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateRegularTimePointProperties(cimRegularTimePoint, rd, importHelper, report);
			}
			return rd;
		}

        private void ImportProtectedSwitch()
        {
            SortedDictionary<string, object> cimProtectedSwitchs = concreteModel.GetAllObjectsOfType("FTN.ProtectedSwitch");
            if (cimProtectedSwitchs != null)
            {
                foreach (KeyValuePair<string, object> cimProtectedSwitchPair in cimProtectedSwitchs)
                {
                    FTN.ProtectedSwitch cimProtectedSwitch = cimProtectedSwitchPair.Value as FTN.ProtectedSwitch;

                    ResourceDescription rd = CreateProtectedSwitchResourceDescription(cimProtectedSwitch);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("ProtectedSwitch ID = ").Append(cimProtectedSwitch.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ProtectedSwitch ID = ").Append(cimProtectedSwitch.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateProtectedSwitchResourceDescription(FTN.ProtectedSwitch cimProtectedSwitch)
		{
			ResourceDescription rd = null;
			if (cimProtectedSwitch != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.PROTECTEDSWITCH, importHelper.CheckOutIndexForDMSType(DMSType.PROTECTEDSWITCH));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimProtectedSwitch.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateProtectedSwitchProperties(cimProtectedSwitch, rd, importHelper, report);
			}
			return rd;
		}

        private void ImportOutageSchedule()
        {
            SortedDictionary<string, object> cimOutageSchedules = concreteModel.GetAllObjectsOfType("FTN.OutageSchedule");
            if (cimOutageSchedules != null)
            {
                foreach (KeyValuePair<string, object> cimOutageSchedulePair in cimOutageSchedules)
                {
                    FTN.OutageSchedule cimOutageSchedule = cimOutageSchedulePair.Value as FTN.OutageSchedule;

                    ResourceDescription rd = CreateOutageScheduletResourceDescription(cimOutageSchedule);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("OutageSchedule ID = ").Append(cimOutageSchedule.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("OutageSchedule ID = ").Append(cimOutageSchedule.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateOutageScheduletResourceDescription(FTN.OutageSchedule cimOutageSchedule)
		{
			ResourceDescription rd = null;
			if (cimOutageSchedule != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.OUTAGESCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.OUTAGESCHEDULE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimOutageSchedule.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateOutageScheduleProperties(cimOutageSchedule, rd, importHelper, report);
			}
			return rd;
		}

        private void ImportRegularIntervalSchedule()
        {
            SortedDictionary<string, object> cimRegularIntervalSchedules = concreteModel.GetAllObjectsOfType("FTN.RegularIntervalSchedule");
            if (cimRegularIntervalSchedules != null)
            {
                foreach (KeyValuePair<string, object> cimRegularIntervalSchedulePair in cimRegularIntervalSchedules)
                {
                    FTN.RegularIntervalSchedule cimRegularIntervalSchedule = cimRegularIntervalSchedulePair.Value as FTN.RegularIntervalSchedule;

                    ResourceDescription rd = CreateRegularIntervalScheduleResourceDescription(cimRegularIntervalSchedule);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegularIntervalSchedule ID = ").Append(cimRegularIntervalSchedule.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegularIntervalSchedule ID = ").Append(cimRegularIntervalSchedule.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }
        private ResourceDescription CreateRegularIntervalScheduleResourceDescription(FTN.RegularIntervalSchedule cimRegularIntervalSchedule)
        {
            ResourceDescription rd = null;
            if (cimRegularIntervalSchedule != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULARINTERVALSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.REGULARINTERVALSCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimRegularIntervalSchedule.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateRegularIntervalScheduleProperties(cimRegularIntervalSchedule, rd, importHelper, report);
            }
            return rd;
        }
        private void ImportIrregularTimePoint()
        {
            SortedDictionary<string, object> cimIrregularTimePoints = concreteModel.GetAllObjectsOfType("FTN.IrregularTimePoint");
            if (cimIrregularTimePoints != null)
            {
                foreach (KeyValuePair<string, object> cimIrregularTimePointsPair in cimIrregularTimePoints)
                {
                    FTN.IrregularTimePoint cimIrregularTimePoint = cimIrregularTimePointsPair.Value as FTN.IrregularTimePoint;

                    ResourceDescription rd = CreateIrregularTimePointResourceDescription(cimIrregularTimePoint);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("IrregularTimePoint ID = ").Append(cimIrregularTimePoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("IrregularTimePoint ID = ").Append(cimIrregularTimePoint.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }
        private ResourceDescription CreateIrregularTimePointResourceDescription(FTN.IrregularTimePoint cimIrregularTimePoint)
        {
            ResourceDescription rd = null;
            if (cimIrregularTimePoint != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.IRREGULARTIMEPOINT, importHelper.CheckOutIndexForDMSType(DMSType.IRREGULARTIMEPOINT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimIrregularTimePoint.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateIrregularTimePointProperties(cimIrregularTimePoint, rd, importHelper, report);
            }
            return rd;
        }

        #endregion Import
    }
}

