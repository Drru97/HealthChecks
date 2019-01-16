using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HealthChecks.Server.Models;

namespace HealthChecks.Server.Services
{
    public class LinuxStorageStatusProvider : IStorageStatusProvider
    {
        private readonly IList<string> _allowedFilesystems = new List<string>
        {
            "ext2", "ext3", "ext4", "fat32", "ntfs"
        };
        
        public async Task<Storage> GetStorageStatusAsync()
        {
            return await Task.FromResult(GetStorage());
        }

        private Storage GetStorage()
        {
            var drives = DriveInfo.GetDrives().Where(d => _allowedFilesystems.Contains(d.DriveFormat));
            var storage = new Storage();

            foreach (var drive in drives)
            {
                var totalSpace = BytesToGigabytes(drive.TotalSize);
                var freeSpace = BytesToGigabytes(drive.TotalFreeSpace);
                var storageStatus = new StorageStatus
                {
                    TotalSpace = totalSpace,
                    AvailableSpace = freeSpace,
                    UsedSpace = totalSpace - freeSpace,
                    CapacityPercentage = GetStoragePercentage(drive.TotalSize, drive.AvailableFreeSpace),
                    Filesystem = drive.DriveFormat,
                    MountedOn = drive.Name
                };

                storage.Storages.Add(storageStatus);
            }

            return storage;
        }

        private static double BytesToGigabytes(long bytes)
        {
            const int bytesInGigabyte = 1024 * 1024 * 1024;

            return (double) bytes / bytesInGigabyte;
        }

        private static int GetStoragePercentage(long totalBytes, long freeBytes)
        {
            if (totalBytes == 0)
            {
                return 0;
            }

            return (int) (100 * (freeBytes / totalBytes));
        }
    }
}
