using System.Collections.Generic;
using System.Linq;
using Checkmary.Checkmarx;
using Checkmary.CxSDKWebService;
using Checkmary.Models;
using ConfigurationSet = Checkmary.Models.ConfigurationSet;
using Preset = Checkmary.Models.Preset;

namespace Checkmary
{
	class CheckmarxProxy
	{
		readonly CheckmarxSoapClient soapClient;
		readonly CheckmarxRestClient restClient;

		public CheckmarxProxy(ProxySettings settings)
		{
			soapClient = new CheckmarxSoapClient(settings);
			restClient = new CheckmarxRestClient(settings);
		}

		public void Initialize()
		{
			soapClient.Login();
			restClient.Login();
		}

		public ProjectSummary[] GetProjectSummaries()
		{
			return soapClient.GetProjectsDisplayData()
				.ToProjectSummaries()
				.ToArray();
		}

		public ProjectSummary FindProjectByName(string name)
		{
			return soapClient.GetProjectsDisplayData()
				.FirstOrDefault(i => i.ProjectName == name)
				.ToProjectSummary();
		}

		public ProjectConfiguration GetProjectConfigurationById(long projectId)
		{
			return soapClient.GetProjectConfiguration(projectId);
		}

		public Preset[] GetPresets()
		{
			return soapClient.GetPresets()
				.ToPresets()
				.ToArray();
		}
		public Preset FindPresetByName(string name)
		{
			return soapClient.GetPresets()
				.FirstOrDefault(i => i.PresetName == name)
				.ToPreset();
		}

		public ConfigurationSet[] GetConfigurationSets()
		{
			return soapClient.GetConfigurationSets()
				.ToConfigurationSets()
				.ToArray();
		}

		public ConfigurationSet FindConfigurationSetByName(string name)
		{
			return soapClient.GetConfigurationSets()
				.FirstOrDefault(i => i.ConfigSetName == name)
				.ToConfigurationSet();
		}

		public Scan StartSastScan(ScanSettings scanSettings)
		{
			return soapClient.Scan(scanSettings.ToCliScanArgs());
		}

	    public OsaScanResponse StartOsaScan(ScanSettings scanSettings)
	    {
	        var scanRestRequest = new OsaScanRequestDto
	        {
	            ProjectId = scanSettings.ProjectId,
	            ZippedSource = scanSettings.ZipFileContents,
                ProjectName = scanSettings.ProjectName
	        };

	        return restClient.Scan(scanRestRequest);
	    }

        public QueuedScanRequest[] GetQueuedScans()
		{
			return restClient.GetScanRequests()
				.Select(i => i.ToQueuedScanRequest())
				.ToArray();
		}

	    public void DownloadOsaScanReport(DownloadOsaScanReportDto reportDto)
	    {
	        restClient.DownloadOsaScanReport(reportDto);
	    }
    }
}