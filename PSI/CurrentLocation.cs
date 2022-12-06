using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI
{
    public static class CurrentLocation
    {
        private static CancellationTokenSource _cancelTokenSource;
        private static double _mockLatitude = 1000;
        private static double _mockLongitude = 1000;

        /*        public static async Task<Location> GetCurrentLocation()
                {
                    try
                    {
                        _isCheckingLocation = true;

                        GeolocationRequest request = new(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                        _cancelTokenSource = new CancellationTokenSource();

                        Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                        return location;
                    }
                    // Catch one of the following exceptions:
                    //   FeatureNotSupportedException
                    //   FeatureNotEnabledException
                    //   PermissionException
                    catch (Exception ex)
                    {
                        // Unable to get location
                    }
                    finally
                    {
                        _isCheckingLocation = false;
                    }
                }*/
        public static Location GetCurrentLocationMock()
        {
            return new Location(_mockLatitude, _mockLongitude);
        }
    }
}
