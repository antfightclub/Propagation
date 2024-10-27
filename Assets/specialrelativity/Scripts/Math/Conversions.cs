namespace SpecialRelativity
{
    public class Conversions
    {
        public static double LightsecondsToMeters(double ls)
        {
            return ls * Constants.c;
        }

        public static double MetersToLightseconds(double meters)
        {
            return meters / Constants.c;
        }

        public static double LightsecondsToLightminutes(double ls)
        {
            return ls / 60;
        }
        public static double LightminutesToLightseconds(double lm)
        {
            return lm * 60;
        }
        public static double LightminutesToLighthours(double lm)
        {
            return lm / 60;
        }
        public static double LighthoursToLightminutes(double lh)
        {
            return lh * 60;
        }
        public static double LightsecondsToLightHours(double ls)
        {
            return ls / 60 / 60;
        }
        public static double LighthoursToLightseconds(double lh)
        {
            return lh * 60 * 60;
        }
    }
}