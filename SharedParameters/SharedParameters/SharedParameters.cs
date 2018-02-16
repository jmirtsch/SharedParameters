using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Reflection;

namespace SharedParameters
{
	public class SharedParameterGroup
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public SharedParameterGroup(int id, string name) { ID = id; Name = name; }
	}
	public enum SharedParameterTypeEnum
	{
		Invalid = 0,
		Text = 1,
		Integer = 2,
		Number = 3,
		Length = 4,
		Area = 5,
		Volume = 6,
		Angle = 7,
		URL = 8,
		Material = 9,
		YesNo = 10,
		Force = 11,
		LinearForce = 12,
		AreaForce = 13,
		Moment = 14,
		NumberOfPoles = 15,
		FixtureUnit = 16,
		FamilyType = 17,
		LoadClassification = 18,
		Image = 19,
		MultilineText = 20,
		HVACDensity = 107,
		HVACEnergy = 108,
		HVACFriction = 109,
		HVACPower = 110,
		HVACPowerDensity = 111,
		HVACPressure = 112,
		HVACTemperature = 113,
		HVACVelocity = 114,
		HVACAirflow = 115,
		HVACDuctSize = 116,
		HVACCrossSection = 117,
		HVACHeatGain = 118,
		ElectricalCurrent = 119,
		ElectricalPotential = 120,
		ElectricalFrequency = 121,
		ElectricalIlluminance = 122,
		ElectricalLuminousFlux = 123,
		ElectricalPower = 124,
		HVACRoughness = 125,
		ElectricalApparentPower = 134,
		ElectricalPowerDensity = 135,
		PipingDensity = 136,
		PipingFlow = 137,
		PipingFriction = 138,
		PipingPressure = 139,
		PipingTemperature = 140,
		PipingVelocity = 141,
		PipingViscosity = 142,
		PipeSize = 143,
		PipingRoughness = 144,
		Stress = 145,
		UnitWeight = 146,
		ThermalExpansion = 147,
		LinearMoment = 148,
		ForcePerLength = 150,
		ForceLengthPerAngle = 151,
		LinearForcePerLength = 152,
		LinearForceLengthPerAngle = 153,
		AreaForcePerLength = 154,
		PipingVolume = 155,
		HVACViscosity = 156,
		HVACCoefficientOfHeatTransfer = 157,
		HVACAirflowDensity = 158,
		Slope = 159,
		HVACCoolingLoad = 160,
		HVACCoolingLoadDividedByArea = 161,
		HVACCoolingLoadDividedByVolume = 162,
		HVACHeatingLoad = 163,
		HVACHeatingLoadDividedByArea = 164,
		HVACHeatingLoadDividedByVolume = 165,
		HVACAirflowDividedByVolume = 166,
		HVACAirflowDividedByCoolingLoad = 167,
		HVACAreaDividedByCoolingLoad = 168,
		WireSize = 169,
		HVACSlope = 170,
		PipingSlope = 171,
		Currency = 172,
		ElectricalEfficacy = 173,
		ElectricalWattage = 174,
		ColorTemperature = 175,
		ElectricalLuminousIntensity = 177,
		ElectricalLuminance = 178,
		HVACAreaDividedByHeatingLoad = 179,
		HVACFactor = 180,
		ElectricalTemperature = 181,
		ElectricalCableTraySize = 182,
		ElectricalConduitSize = 183,
		ReinforcementVolume = 184,
		ReinforcementLength = 185,
		ElectricalDemandFactor = 186,
		HVACDuctInsulationThickness = 187,
		HVACDuctLiningThickness = 188,
		PipeInsulationThickness = 189,
		HVACThermalResistance = 190,
		HVACThermalMass = 191,
		Acceleration = 192,
		BarDiameter = 193,
		CrackWidth = 194,
		DisplacementDeflection = 195,
		Energy = 196,
		StructuralFrequency = 197,
		Mass = 198,
		MassPerUnitLength = 199,
		MomentOfInertia = 200,
		SurfaceArea = 201,
		Period = 202,
		Pulsation = 203,
		ReinforcementArea = 204,
		ReinforcementAreaPerUnitLength = 205,
		ReinforcementCover = 206,
		ReinforcementSpacing = 207,
		Rotation = 208,
		SectionArea = 209,
		SectionDimension = 210,
		SectionModulus = 211,
		SectionProperty = 212,
		StructuralVelocity = 213,
		WarpingConstant = 214,
		Weight = 215,
		WeightPerUnitLength = 216,
		HVACThermalConductivity = 217,
		HVACSpecificHeat = 218,
		HVACSpecificHeatOfVaporization = 219,
		HVACPermeability = 220,
		ElectricalResistivity = 221,
		MassDensity = 222,
		MassPerUnitArea = 223,
		PipeDimension = 224,
		PipeMass = 225,
		PipeMassPerUnitLength = 226,
		HVACTemperatureDifference = 227,
		PipingTemperatureDifference = 228,
		ElectricalTemperatureDifference = 229
	}
	public class SharedParameter
	{
		public string Name { get; set; }
		public Guid Id { get; set; }
		public SharedParameterTypeEnum DataType { get; set; } = SharedParameterTypeEnum.Text;
		public int DataCategory { get; set; }
		public SharedParameterGroup Group { get; set; }
		public bool Visible { get; set; } = true;
		public string Description { get; set; } = "";
		public bool Modifiable { get; set; } = true;
		public SharedParameter(Guid id, string name, SharedParameterTypeEnum type, int category, SharedParameterGroup group)
		{
			Id = id;
			Name = name;
			DataType = type;
			DataCategory = category;
			Group = group;
		}
	}
	public class SharedParameterFile
	{
		public static string GenerateSharedParameterFile(List<SharedParameterGroup> groups, List<SharedParameter> parameters)
		{
			AssemblyName name = Assembly.GetCallingAssembly().GetName();
			string date = String.Format("{0:s}", System.IO.File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToUniversalTime());
			string result = "# This is a Revit shared parameter file, generated " + DateTime.Now.ToShortDateString() + " using " + name.Name + " v" + name.Version.ToString() + " built " + date + "\r\n";
			result += "# Do not edit manually.\r\n";
			result += "*META\tVERSION\tMINVERSION\r\n";
			result += "META\t2\t1\r\n";
			result += "*GROUP\tID\tNAME\r\n";
			foreach (SharedParameterGroup group in groups)
				result += "GROUP\t" + group.ID + "\t" + group.Name + "\r\n";
			result += "*PARAM\tGUID\tNAME\tDATATYPE\tDATACATEGORY\tGROUP\tVISIBLE\tDESCRIPTION\tUSERMODIFIABLE\r\n";
			foreach (SharedParameter parameter in parameters)
				result += "PARAM\t" + parameter.Id.ToString() + "\t" + parameter.Name + "\t" + parameter.DataType.ToString().ToUpper() + "\t\t" + parameter.Group.ID + (parameter.Visible ? "\t1\t" : "\t0\t") + Regex.Replace(parameter.Description, @"\t|\n|\r", "") + "\t" + (parameter.Modifiable ? "1" : "") + "\r\n";

			return result;
		}
	}
}
