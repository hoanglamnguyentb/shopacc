using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using System.Collections.Generic;

namespace Hinet.Web.Common

{
    public class GisExtension
    {
        public static double[] ChuyenToaDoWGS84ToUTM(double[] Point, string OGC_WKT = null)
        {
            CoordinateSystemFactory csFact = new CoordinateSystemFactory();
            CoordinateTransformationFactory ctFact = new CoordinateTransformationFactory();
            if (string.IsNullOrEmpty(OGC_WKT))
                OGC_WKT = "PROJCS[\"VN-2000 / TM-3 zone 482\",\r\n    GEOGCS[\"VN-2000\",\r\n        DATUM[\"Vietnam_2000\",\r\n            SPHEROID[\"WGS 84\",6378137,298.257223563],\r\n            TOWGS84[-191.90441429,-39.30318279,-111.45032835,-0.00928836,0.01975479,-0.00427372,0.252906278]],\r\n        PRIMEM[\"Greenwich\",0,\r\n            AUTHORITY[\"EPSG\",\"8901\"]],\r\n        UNIT[\"degree\",0.0174532925199433,\r\n            AUTHORITY[\"EPSG\",\"9122\"]],\r\n        AUTHORITY[\"EPSG\",\"4756\"]],\r\n    PROJECTION[\"Transverse_Mercator\"],\r\n    PARAMETER[\"latitude_of_origin\",0],\r\n    PARAMETER[\"central_meridian\",105],\r\n    PARAMETER[\"scale_factor\",0.9999],\r\n    PARAMETER[\"false_easting\",500000],\r\n    PARAMETER[\"false_northing\",0],\r\n    UNIT[\"metre\",1,\r\n        AUTHORITY[\"EPSG\",\"9001\"]],\r\n    AXIS[\"Easting\",EAST],\r\n    AXIS[\"Northing\",NORTH],\r\n    AUTHORITY[\"EPSG\",\"5897\"]]";
            CoordinateSystem ESPG = csFact.CreateFromWkt(OGC_WKT);
            GeographicCoordinateSystem WGS84 = GeographicCoordinateSystem.WGS84;
            ICoordinateTransformation trans = ctFact.CreateFromCoordinateSystems(WGS84, ESPG);
            double[] rst = trans.MathTransform.Transform(Point);
            return rst;
        }

        public static List<double[]> ChuyenListToaDoWGS84ToUTM(List<double[]> lPoints, string OGC_WKT = null)
        {
            CoordinateSystemFactory csFact = new CoordinateSystemFactory();
            CoordinateTransformationFactory ctFact = new CoordinateTransformationFactory();

            if (string.IsNullOrEmpty(OGC_WKT))
                OGC_WKT = "PROJCS[\"VN-2000 / TM-3 zone 482\",\r\n    GEOGCS[\"VN-2000\",\r\n        DATUM[\"Vietnam_2000\",\r\n            SPHEROID[\"WGS 84\",6378137,298.257223563],\r\n            TOWGS84[-191.90441429,-39.30318279,-111.45032835,-0.00928836,0.01975479,-0.00427372,0.252906278]],\r\n        PRIMEM[\"Greenwich\",0,\r\n            AUTHORITY[\"EPSG\",\"8901\"]],\r\n        UNIT[\"degree\",0.0174532925199433,\r\n            AUTHORITY[\"EPSG\",\"9122\"]],\r\n        AUTHORITY[\"EPSG\",\"4756\"]],\r\n    PROJECTION[\"Transverse_Mercator\"],\r\n    PARAMETER[\"latitude_of_origin\",0],\r\n    PARAMETER[\"central_meridian\",105],\r\n    PARAMETER[\"scale_factor\",0.9999],\r\n    PARAMETER[\"false_easting\",500000],\r\n    PARAMETER[\"false_northing\",0],\r\n    UNIT[\"metre\",1,\r\n        AUTHORITY[\"EPSG\",\"9001\"]],\r\n    AXIS[\"Easting\",EAST],\r\n    AXIS[\"Northing\",NORTH],\r\n    AUTHORITY[\"EPSG\",\"5897\"]]";
            CoordinateSystem ESPG = csFact.CreateFromWkt(OGC_WKT);

            GeographicCoordinateSystem WGS84 = GeographicCoordinateSystem.WGS84;

            ICoordinateTransformation trans = ctFact.CreateFromCoordinateSystems(WGS84, ESPG);

            var result = new List<double[]>();
            for (int i = 0; i < lPoints.Count; i++)
            {
                double[] rst = trans.MathTransform.Transform(lPoints[i]);
                result.Add(rst);
            }

            return result;
        }

        public static List<double[]> ChuyenListToaDoSangWGS84(List<double[]> lPoints, string OGC_WKT = null)
        {
            CoordinateSystemFactory csFact = new CoordinateSystemFactory();
            CoordinateTransformationFactory ctFact = new CoordinateTransformationFactory();

            if (string.IsNullOrEmpty(OGC_WKT))
                OGC_WKT = "PROJCS[\"VN-2000 / TM-3 zone 482\",\r\n    GEOGCS[\"VN-2000\",\r\n        DATUM[\"Vietnam_2000\",\r\n            SPHEROID[\"WGS 84\",6378137,298.257223563],\r\n            TOWGS84[-191.90441429,-39.30318279,-111.45032835,-0.00928836,0.01975479,-0.00427372,0.252906278]],\r\n        PRIMEM[\"Greenwich\",0,\r\n            AUTHORITY[\"EPSG\",\"8901\"]],\r\n        UNIT[\"degree\",0.0174532925199433,\r\n            AUTHORITY[\"EPSG\",\"9122\"]],\r\n        AUTHORITY[\"EPSG\",\"4756\"]],\r\n    PROJECTION[\"Transverse_Mercator\"],\r\n    PARAMETER[\"latitude_of_origin\",0],\r\n    PARAMETER[\"central_meridian\",105],\r\n    PARAMETER[\"scale_factor\",0.9999],\r\n    PARAMETER[\"false_easting\",500000],\r\n    PARAMETER[\"false_northing\",0],\r\n    UNIT[\"metre\",1,\r\n        AUTHORITY[\"EPSG\",\"9001\"]],\r\n    AXIS[\"Easting\",EAST],\r\n    AXIS[\"Northing\",NORTH],\r\n    AUTHORITY[\"EPSG\",\"5897\"]]";

            CoordinateSystem ESPG = csFact.CreateFromWkt(OGC_WKT);

            GeographicCoordinateSystem WGS84 = GeographicCoordinateSystem.WGS84;

            ICoordinateTransformation trans = ctFact.CreateFromCoordinateSystems(ESPG, WGS84);

            var result = new List<double[]>();
            for (int i = 0; i < lPoints.Count; i++)
            {
                double[] rst = trans.MathTransform.Transform(lPoints[i]);
                result.Add(rst);
            }

            return result;
        }

        public static double[] ChuyenToaDoSangWGS84(double[] Point, string OGC_WKT = null)
        {
            CoordinateSystemFactory csFact = new CoordinateSystemFactory();
            CoordinateTransformationFactory ctFact = new CoordinateTransformationFactory();

            if (string.IsNullOrEmpty(OGC_WKT))
                OGC_WKT = "PROJCS[\"VN-2000 / TM-3 zone 482\",\r\n    GEOGCS[\"VN-2000\",\r\n        DATUM[\"Vietnam_2000\",\r\n            SPHEROID[\"WGS 84\",6378137,298.257223563],\r\n            TOWGS84[-191.90441429,-39.30318279,-111.45032835,-0.00928836,0.01975479,-0.00427372,0.252906278]],\r\n        PRIMEM[\"Greenwich\",0,\r\n            AUTHORITY[\"EPSG\",\"8901\"]],\r\n        UNIT[\"degree\",0.0174532925199433,\r\n            AUTHORITY[\"EPSG\",\"9122\"]],\r\n        AUTHORITY[\"EPSG\",\"4756\"]],\r\n    PROJECTION[\"Transverse_Mercator\"],\r\n    PARAMETER[\"latitude_of_origin\",0],\r\n    PARAMETER[\"central_meridian\",105],\r\n    PARAMETER[\"scale_factor\",0.9999],\r\n    PARAMETER[\"false_easting\",500000],\r\n    PARAMETER[\"false_northing\",0],\r\n    UNIT[\"metre\",1,\r\n        AUTHORITY[\"EPSG\",\"9001\"]],\r\n    AXIS[\"Easting\",EAST],\r\n    AXIS[\"Northing\",NORTH],\r\n    AUTHORITY[\"EPSG\",\"5897\"]]";

            CoordinateSystem ESPG = csFact.CreateFromWkt(OGC_WKT);

            GeographicCoordinateSystem WGS84 = GeographicCoordinateSystem.WGS84;

            ICoordinateTransformation trans = ctFact.CreateFromCoordinateSystems(ESPG, WGS84);

            double[] rst = trans.MathTransform.Transform(Point);

            return rst;
        }
    }
}