using System;
using System.Collections.Generic;

namespace BioContracts.Stream
{
  public interface IMediaSettings
  {
    void ShowSettings(IntPtr parentWindow);
    void SetFormat(IMediaFormat format);
    List<IMediaFormat> Formats();
  }

  public interface IMediaFormat
  {
    int FrameWidth  { get; }
    int FrameHeight { get; }
    int FrameRate  { get; }
  }

  public class BasicMediaFormat : IMediaFormat
  {
    public int FrameWidth  { get; set; }
    public int FrameHeight { get; set; }
    public int FrameRate   { get; set; }
  }
}
