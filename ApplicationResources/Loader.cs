using System.Windows.Media.Imaging;
using ApplicationResources.Properties;
using Utils;

namespace ApplicationResources
{
  public class Loader
  {
    private static readonly object SyncObject = new object();

    private static volatile Loader _instance;
    public static Loader Instance
    {
      get
      {
        if (_instance != null) return _instance;
        lock (SyncObject)
        {
          if (_instance == null)
            _instance = new Loader();
        }
        return _instance;
      }
    }

    private Loader() { }

    private static BitmapSource _expandIcon;
    private static BitmapSource _warningIcon;
    private static BitmapSource _plusIcon;
    private static BitmapSource _startIcon;
    private static BitmapSource _stopIcon;
    private static BitmapSource _helpDialogIcon;
    private static BitmapSource _refreshIcon;
    private static BitmapSource _uploadIcon;
    private static BitmapSource _errorIcon;
    private static BitmapSource _okIcon;
    private static BitmapSource _deleteIcon;
    private static BitmapSource _removeIcon;
    private static BitmapSource _addIcon;
    private static BitmapSource _cancelIcon;
    private static BitmapSource _informationCircleIcon;
    private static BitmapSource _rightArrowIcon;
    private static BitmapSource _leftArrowIcon;
    private static BitmapSource _downArrowIcon;
    private static BitmapSource _infoDialogIcon;
    private static BitmapSource _sendIcons;
    private static BitmapSource _receiveIcons;
    private static BitmapSource _minusIcons;
    private static BitmapSource _actionIcons;
    private static BitmapSource _faceIcons;
    private static BitmapSource _logoIcon;

    private static BitmapSource _addUserIcon        ;
    private static BitmapSource _addLocationIcon    ;
    private static BitmapSource _bothHandsIcon      ;
    private static BitmapSource _cardInUseIcon      ;
    private static BitmapSource _contactCardIcon    ;
    private static BitmapSource _disconnectedIcon   ;
    private static BitmapSource _facialScanIcon     ;
    private static BitmapSource _fingerScanIcon     ;
    private static BitmapSource _fingerprintScanIcon;
    private static BitmapSource _homeIcon           ;
    private static BitmapSource _infoIcon           ;
    private static BitmapSource _irisScanIcon       ;
    private static BitmapSource _irisScanImageIcon  ;
    private static BitmapSource _journalListIcon    ;
    private static BitmapSource _loaderIcon         ;
    private static BitmapSource _loadPhotosIcon     ;
    private static BitmapSource _notVerifiedIcon    ;
    private static BitmapSource _screenshootIcon    ;
    private static BitmapSource _settingsIcon       ;
    private static BitmapSource _snapshootIcon      ;
    private static BitmapSource _thumbnailIcon      ;
    private static BitmapSource _trackingIcon       ;
    private static BitmapSource _userImageIcon      ;
    private static BitmapSource _usersListIcon      ;
    private static BitmapSource _verifiedIcon       ;

    public static BitmapSource AddUserIcon        => _addUserIcon         ?? (_addUserIcon         = BitmapConversion.BitmapToBitmapSource(Resources.add_user_icon));
    public static BitmapSource AddLocationIcon     => _addLocationIcon     ?? (_addLocationIcon     = BitmapConversion.BitmapToBitmapSource(Resources.add_location_icon));
    public static BitmapSource BothHandsIcon       => _bothHandsIcon       ?? (_bothHandsIcon       = BitmapConversion.BitmapToBitmapSource(Resources.bothHands_icon));
    public static BitmapSource CardInUseIcon       => _cardInUseIcon       ?? (_cardInUseIcon       = BitmapConversion.BitmapToBitmapSource(Resources.card_in_use_icon));
    public static BitmapSource ContactCardIcon     => _contactCardIcon     ?? (_contactCardIcon     = BitmapConversion.BitmapToBitmapSource(Resources.contact_card_icon));
    public static BitmapSource DisconnectedIcon    => _disconnectedIcon    ?? (_disconnectedIcon    = BitmapConversion.BitmapToBitmapSource(Resources.disconnected_icon));
    public static BitmapSource FacialScanIcon      => _facialScanIcon      ?? (_facialScanIcon      = BitmapConversion.BitmapToBitmapSource(Resources.facial_scan_icon));
    public static BitmapSource FingerScanIcon      => _fingerScanIcon      ?? (_fingerScanIcon      = BitmapConversion.BitmapToBitmapSource(Resources.finger_scan_icon));
    public static BitmapSource FingerprintScanIcon => _fingerprintScanIcon ?? (_fingerprintScanIcon = BitmapConversion.BitmapToBitmapSource(Resources.fingerprint_scan_icon));
    public static BitmapSource HomeIcon            => _homeIcon            ?? (_homeIcon            = BitmapConversion.BitmapToBitmapSource(Resources.home_icon));
    public static BitmapSource InfoIcon            => _infoIcon            ?? (_infoIcon            = BitmapConversion.BitmapToBitmapSource(Resources.info_icon));
    public static BitmapSource IrisScanIcon        => _irisScanIcon        ?? (_irisScanIcon        = BitmapConversion.BitmapToBitmapSource(Resources.iris_scan_icon));
    public static BitmapSource IrisScanImageIcon   => _irisScanImageIcon   ?? (_irisScanImageIcon   = BitmapConversion.BitmapToBitmapSource(Resources.iris_scan_image_icon));
    public static BitmapSource JournalListIcon     => _journalListIcon     ?? (_journalListIcon     = BitmapConversion.BitmapToBitmapSource(Resources.journal_list_icon));
    public static BitmapSource LoaderIcon          => _loaderIcon          ?? (_loaderIcon          = BitmapConversion.BitmapToBitmapSource(Resources.loader_icon));
    public static BitmapSource LoadPhotosIcon      => _loadPhotosIcon      ?? (_loadPhotosIcon      = BitmapConversion.BitmapToBitmapSource(Resources.loadphotos_icon));
    public static BitmapSource NotVerifiedIcon     => _notVerifiedIcon     ?? (_notVerifiedIcon     = BitmapConversion.BitmapToBitmapSource(Resources.not_verified_icon));
    public static BitmapSource ScreenshootIcon     => _screenshootIcon     ?? (_screenshootIcon     = BitmapConversion.BitmapToBitmapSource(Resources.screenshot_icon));
    public static BitmapSource SettingsIcon        => _settingsIcon        ?? (_settingsIcon        = BitmapConversion.BitmapToBitmapSource(Resources.settings_icon));
    public static BitmapSource SnapshootIcon       => _snapshootIcon       ?? (_snapshootIcon       = BitmapConversion.BitmapToBitmapSource(Resources.snapshoot_icon));
    public static BitmapSource ThumbnailIcon       => _thumbnailIcon       ?? (_thumbnailIcon       = BitmapConversion.BitmapToBitmapSource(Resources.thumnail_icon));
    public static BitmapSource TrackingIcon        => _trackingIcon        ?? (_trackingIcon        = BitmapConversion.BitmapToBitmapSource(Resources.tracking_icon));
    public static BitmapSource UserImageIcon       => _userImageIcon       ?? (_userImageIcon       = BitmapConversion.BitmapToBitmapSource(Resources.user_image_icon));
    public static BitmapSource UsersListIcon       => _usersListIcon       ?? (_usersListIcon       = BitmapConversion.BitmapToBitmapSource(Resources.users_list_icon));
    public static BitmapSource VerifiedIcon        => _verifiedIcon        ?? (_verifiedIcon        = BitmapConversion.BitmapToBitmapSource(Resources.verified_icon));

                               
    //
    public static BitmapSource LogoIcon              => _logoIcon     ?? (_logoIcon     = BitmapConversion.BitmapToBitmapSource(Resources.logo.ToBitmap()));
    public static BitmapSource FaceIcons             => _faceIcons    ?? (_faceIcons    = BitmapConversion.BitmapToBitmapSource(Resources.face_icon));
    public static BitmapSource ActionIcons           => _actionIcons  ?? (_actionIcons  = BitmapConversion.BitmapToBitmapSource(Resources.action_icon));
    public static BitmapSource MinusIcons            => _minusIcons   ?? (_minusIcons   = BitmapConversion.BitmapToBitmapSource(Resources.minus_icon));
    public static BitmapSource ReceiveIcons          => _receiveIcons ?? (_receiveIcons = BitmapConversion.BitmapToBitmapSource(Resources.receive_icon));
    public static BitmapSource SendIcon              => _sendIcons    ?? (_sendIcons    = BitmapConversion.BitmapToBitmapSource(Resources.send_icon));
    public static BitmapSource ExpandIcon            => _expandIcon   ?? (_expandIcon   = BitmapConversion.BitmapToBitmapSource(Resources.expand_icon));
    public static BitmapSource WarningIcon           => _warningIcon  ?? (_warningIcon  = BitmapConversion.BitmapToBitmapSource(Resources.warning_icon));
    public static BitmapSource PlusIcon              => _plusIcon     ?? (_plusIcon     = BitmapConversion.BitmapToBitmapSource(Resources.plus_icon));
    public static BitmapSource StartIcon             => _startIcon    ?? (_startIcon    = BitmapConversion.BitmapToBitmapSource(Resources.Start_icon));
    public static BitmapSource StopIcon              => _stopIcon     ?? (_stopIcon     = BitmapConversion.BitmapToBitmapSource(Resources.Stop_icon));
    public static BitmapSource HelpDialogIcon        => _helpDialogIcon ??
                                                              (_helpDialogIcon = BitmapConversion.BitmapToBitmapSource(Resources.help_dialog_icon));
    public static BitmapSource InfoDialogIcon        => _infoDialogIcon ??
                                                              (_infoDialogIcon = BitmapConversion.BitmapToBitmapSource(Resources.info_dialog_icon));
    public static BitmapSource DownArrowIcon         => _downArrowIcon ??
                                                              (_downArrowIcon = BitmapConversion.BitmapToBitmapSource(Resources.down_arrow_icon));
    public static BitmapSource LeftArrowIcon         => _leftArrowIcon ??
                                                              (_leftArrowIcon = BitmapConversion.BitmapToBitmapSource(Resources.left_arrow_icon));
    public static BitmapSource RightArrowIcon        => _rightArrowIcon ??
                                                              (_rightArrowIcon = BitmapConversion.BitmapToBitmapSource(Resources.right_arrow_icon));
    public static BitmapSource InformationCircleIcon => _informationCircleIcon ??
                                                              (_informationCircleIcon = BitmapConversion.BitmapToBitmapSource(Resources.info_circle_icon));
    public static BitmapSource CancelIcon            => _cancelIcon  ?? (_cancelIcon  = BitmapConversion.BitmapToBitmapSource(Resources.cancel_icon));
    public static BitmapSource AddIcon               => _addIcon     ?? (_addIcon     = BitmapConversion.BitmapToBitmapSource(Resources.add_icon));
    public static BitmapSource RemoveIcon            => _removeIcon  ?? (_removeIcon  = BitmapConversion.BitmapToBitmapSource(Resources.remove_icon));
    public static BitmapSource DeleteIcon            => _deleteIcon  ?? (_deleteIcon  = BitmapConversion.BitmapToBitmapSource(Resources.delete_icon));
    public static BitmapSource OkIcon                => _okIcon      ?? (_okIcon      = BitmapConversion.BitmapToBitmapSource(Resources.ok_icon));
    public static BitmapSource ErrorIcon             => _errorIcon   ?? (_errorIcon   = BitmapConversion.BitmapToBitmapSource(Resources.error_icon));
    public static BitmapSource UploadIcon            => _uploadIcon  ?? (_uploadIcon  = BitmapConversion.BitmapToBitmapSource(Resources.upload_icon));
    public static BitmapSource RefreshIcon           => _refreshIcon ?? (_refreshIcon = BitmapConversion.BitmapToBitmapSource(Resources.refresh_icon));
  }
}
