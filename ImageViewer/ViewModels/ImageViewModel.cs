using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using Castle.Windsor;
using ImageViewer.Contracts;
using Utils;

namespace ImageViewer.ViewModels
{
  public class ImageViewModel : Conductor<IImageItemScreen>.Collection.OneActive
                              , IImageItemCollection
  {
    public ImageViewModel(IWindsorContainer container)
    {
      _container = container;
      _flags     = new Flags<ImageViewStyle>();

      ZoomToFitState = true;
    }
    
    public virtual void Clear()
    {
      Items.Clear();
      RefreshUi();
    }

    public virtual void AddItem(IImageItemScreen itemScreen = null)
    {
      if (itemScreen == null) itemScreen = new ImageItemViewModel(_container);
      Items.Add(itemScreen);
      MakeActive(itemScreen);
    }

    public virtual void RemoveItem(IImageItemScreen itemScreen = null)
    {
      if (itemScreen == null)
        itemScreen = ActiveItem;

      Items.Remove(itemScreen);
      SelectDefault();
    }

    private void SelectDefault()
    {
      MakeActive(Items.LastOrDefault());
    }
    
    public void MakeActive(IImageItemScreen itemScreen)
    {
      if (ActiveItem == itemScreen) return;
      ActivateItem(itemScreen);
      ActiveItem?.Activate();
      RefreshUi();
    }

    public int Count => Items.Count;

    public void Subscribe(NotifyCollectionChangedEventHandler eventHandler)
    {
      Items.CollectionChanged += eventHandler;
    }

    public void Unsubscribe(NotifyCollectionChangedEventHandler eventHandler)
    {
      Items.CollectionChanged -= eventHandler;
    }

    public void Zoom(double viewWidth, double viewHeight)
    {
      if ( viewWidth.Equals(_scrollFieldWidth) 
        && viewHeight.Equals(_scrollFieldHeight ))
        return;

      _scrollFieldWidth = viewWidth;
      _scrollFieldHeight = viewHeight;

      Zoom(ZoomRate);
    }

    public void Zoom(double zoomRate)
    {
      try
      {
        var zR = zoomRate / MaxPercentage;
        foreach (var imageItem in Items)
          imageItem.Zoom(_scrollFieldWidth * zR, _scrollFieldHeight * zR);
      }
      catch (TaskCanceledException) { }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }


    private void RefreshUi()
    {
      NotifyOfPropertyChange(() => Items);
      Zoom(ZoomRate);
    }
    
    private double _zoomRate;
    public double ZoomRate
    {
      get { return _zoomRate; }
      set
      {
        if (_zoomRate.Equals(value)) return;
        _zoomRate = value;

        ZoomToFitState = (_zoomRate.Equals(ZoomToFitRate));
        Zoom(_zoomRate);
        NotifyOfPropertyChange(() => ZoomRate);
      }
    }

    private bool _zoomToFitState;
    public bool ZoomToFitState
    {
      get { return _zoomToFitState; }
      set
      {
        if (_zoomToFitState == value) return;
        _zoomToFitState = value;
        ZoomRate = _zoomToFitState ? ZoomToFitRate : ZoomRate;
        NotifyOfPropertyChange(() => ZoomToFitState);
      }
    }
    
    private bool _isExpanded;
    public bool IsExpanded
    {
      get { return _isExpanded; }
      set
      {
        if (_isExpanded == value) return;
        _isExpanded = value;
        NotifyOfPropertyChange(() => IsExpanded);
      }
    }

    private int _currentStyle;
    public int CurrentStyle
    {
      get { return _currentStyle; }
      set
      {
        if (_currentStyle == value) return;
        _currentStyle = value;
        NotifyOfPropertyChange(() => CurrentStyle);
      }
    }

    //TODO to multibinding
    public bool ExpandButtonVisibility     => true;// _flags.Has(CurrentStyle, ExpandButton, true);
    public bool SendButtonVisibility       => true;// _flags.Has(CurrentStyle, SendButton, true);
    public bool UploadManyButtonVisibility => true;// _flags.Has(CurrentStyle, UploadManyButton, true);
    public bool MinusButtonVisibility      => true;// _flags.Has(CurrentStyle, MinusButton, true);
    public bool PlusButtonVisibility       => true;// _flags.Has(CurrentStyle, PlusButton, true);
    public bool SliderPanelVisibility      => true;// _flags.Has(CurrentStyle, SliderPanel, true);

    private double _scrollFieldWidth;
    private double _scrollFieldHeight;

    private const double ZoomToFitRate = 99 ;
    private const double MaxPercentage = 100;

    private readonly IWindsorContainer     _container;
    private readonly Flags<ImageViewStyle> _flags    ;
  }

  [Flags]
  public enum ImageViewStyle
  {
     PlusButton       = 1 
   , MinusButton      = 1 << 1
   , UploadManyButton = 1 << 2
   , SendButton       = 1 << 3
   , ExpandButton     = 1 << 4
   , SliderPanel      = 1 << 5
  }
}
