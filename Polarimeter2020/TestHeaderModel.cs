using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TestHeaderModel
{
    public string GpibAddressOfDmm34401A { get; set; }
    public string GpibAddressOfDmm34401AHeader { get { return "DMM 34401A GPIB Address"; } }
    public string GpibAddressOfMmc2 { get; set; }
    public string GpibAddressOfMmc2Header { get { return "MMC-2 GPIB Address"; } }
    public string SampleName { get; set; }
    public string SampleNameHeader { get { return "Sample Name"; } }
    public int NumberOfSamples { get; set; }
    public string NumberOfSamplesHeader { get { return "Number of Samples"; } }
    public int NumberOfRotations { get; set; }
    public string NumberOfRotationsHeader { get { return "Number of Rotations"; } }
    public int AverageNumber { get; set; }
    public string AverageNumberHeader { get { return "Average Number"; } }
    public double Resolution { get; set; }
    public string ResolutionHeader { get { return "Resolution"; } }
}

