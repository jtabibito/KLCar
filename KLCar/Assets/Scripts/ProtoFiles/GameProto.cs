//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: GameProto.proto
namespace MyGameProto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MyPlayerInfo")]
  public partial class MyPlayerInfo : global::ProtoBuf.IExtensible
  {
    public MyPlayerInfo() {}
    
    private int _userID;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"userID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int userID
    {
      get { return _userID; }
      set { _userID = value; }
    }
    private string _userName;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"userName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string userName
    {
      get { return _userName; }
      set { _userName = value; }
    }
    private long _updateTime;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"updateTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long updateTime
    {
      get { return _updateTime; }
      set { _updateTime = value; }
    }
    private string _userSex = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"userSex", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string userSex
    {
      get { return _userSex; }
      set { _userSex = value; }
    }
    private string _deviceCode = "";
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"deviceCode", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string deviceCode
    {
      get { return _deviceCode; }
      set { _deviceCode = value; }
    }
    private string _nickname = "";
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"nickname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string nickname
    {
      get { return _nickname; }
      set { _nickname = value; }
    }
    private int _deviceType = default(int);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"deviceType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int deviceType
    {
      get { return _deviceType; }
      set { _deviceType = value; }
    }
    private string _nowCarId = "";
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"nowCarId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string nowCarId
    {
      get { return _nowCarId; }
      set { _nowCarId = value; }
    }
    private string _nowRoleId = "";
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"nowRoleId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string nowRoleId
    {
      get { return _nowRoleId; }
      set { _nowRoleId = value; }
    }
    private string _nowPetId = "";
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"nowPetId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string nowPetId
    {
      get { return _nowPetId; }
      set { _nowPetId = value; }
    }
    private string _nowInputName = "";
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"nowInputName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string nowInputName
    {
      get { return _nowInputName; }
      set { _nowInputName = value; }
    }
    private long _registTime = default(long);
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"registTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long registTime
    {
      get { return _registTime; }
      set { _registTime = value; }
    }
    private long _lastOnlineTime = default(long);
    [global::ProtoBuf.ProtoMember(13, IsRequired = false, Name=@"lastOnlineTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long lastOnlineTime
    {
      get { return _lastOnlineTime; }
      set { _lastOnlineTime = value; }
    }
    private long _gold = default(long);
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, Name=@"gold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long gold
    {
      get { return _gold; }
      set { _gold = value; }
    }
    private long _diamond = default(long);
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"diamond", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long diamond
    {
      get { return _diamond; }
      set { _diamond = value; }
    }
    private long _power = default(long);
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"power", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long power
    {
      get { return _power; }
      set { _power = value; }
    }
    private long _score = default(long);
    [global::ProtoBuf.ProtoMember(17, IsRequired = false, Name=@"score", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long score
    {
      get { return _score; }
      set { _score = value; }
    }
    private int _selectRoleSexFlag = default(int);
    [global::ProtoBuf.ProtoMember(18, IsRequired = false, Name=@"selectRoleSexFlag", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int selectRoleSexFlag
    {
      get { return _selectRoleSexFlag; }
      set { _selectRoleSexFlag = value; }
    }
    private int _userRoleImgID = default(int);
    [global::ProtoBuf.ProtoMember(19, IsRequired = false, Name=@"userRoleImgID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int userRoleImgID
    {
      get { return _userRoleImgID; }
      set { _userRoleImgID = value; }
    }
    private string _missionOfJuqing = "";
    [global::ProtoBuf.ProtoMember(20, IsRequired = false, Name=@"missionOfJuqing", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string missionOfJuqing
    {
      get { return _missionOfJuqing; }
      set { _missionOfJuqing = value; }
    }
    private long _timeOfRichang = default(long);
    [global::ProtoBuf.ProtoMember(21, IsRequired = false, Name=@"timeOfRichang", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long timeOfRichang
    {
      get { return _timeOfRichang; }
      set { _timeOfRichang = value; }
    }
    private int _totalDistance = default(int);
    [global::ProtoBuf.ProtoMember(22, IsRequired = false, Name=@"totalDistance", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int totalDistance
    {
      get { return _totalDistance; }
      set { _totalDistance = value; }
    }
    private int _totalPickGold = default(int);
    [global::ProtoBuf.ProtoMember(23, IsRequired = false, Name=@"totalPickGold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int totalPickGold
    {
      get { return _totalPickGold; }
      set { _totalPickGold = value; }
    }
    private int _totalHitTimes = default(int);
    [global::ProtoBuf.ProtoMember(24, IsRequired = false, Name=@"totalHitTimes", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int totalHitTimes
    {
      get { return _totalHitTimes; }
      set { _totalHitTimes = value; }
    }
    private int _totalHitByYouche = default(int);
    [global::ProtoBuf.ProtoMember(25, IsRequired = false, Name=@"totalHitByYouche", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int totalHitByYouche
    {
      get { return _totalHitByYouche; }
      set { _totalHitByYouche = value; }
    }
    private int _totalHitByJinche = default(int);
    [global::ProtoBuf.ProtoMember(26, IsRequired = false, Name=@"totalHitByJinche", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int totalHitByJinche
    {
      get { return _totalHitByJinche; }
      set { _totalHitByJinche = value; }
    }
    private int _totalPickItems = default(int);
    [global::ProtoBuf.ProtoMember(27, IsRequired = false, Name=@"totalPickItems", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int totalPickItems
    {
      get { return _totalPickItems; }
      set { _totalPickItems = value; }
    }
    private int _totalSpeedUp = default(int);
    [global::ProtoBuf.ProtoMember(28, IsRequired = false, Name=@"totalSpeedUp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int totalSpeedUp
    {
      get { return _totalSpeedUp; }
      set { _totalSpeedUp = value; }
    }
    private int _totalUseDaodan = default(int);
    [global::ProtoBuf.ProtoMember(29, IsRequired = false, Name=@"totalUseDaodan", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int totalUseDaodan
    {
      get { return _totalUseDaodan; }
      set { _totalUseDaodan = value; }
    }
    private int _totalUseJiasu = default(int);
    [global::ProtoBuf.ProtoMember(30, IsRequired = false, Name=@"totalUseJiasu", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int totalUseJiasu
    {
      get { return _totalUseJiasu; }
      set { _totalUseJiasu = value; }
    }
    private int _totalUseYinshen = default(int);
    [global::ProtoBuf.ProtoMember(31, IsRequired = false, Name=@"totalUseYinshen", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int totalUseYinshen
    {
      get { return _totalUseYinshen; }
      set { _totalUseYinshen = value; }
    }
    private int _totalUseHudun = default(int);
    [global::ProtoBuf.ProtoMember(32, IsRequired = false, Name=@"totalUseHudun", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int totalUseHudun
    {
      get { return _totalUseHudun; }
      set { _totalUseHudun = value; }
    }
    private int _totalUsePet = default(int);
    [global::ProtoBuf.ProtoMember(33, IsRequired = false, Name=@"totalUsePet", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int totalUsePet
    {
      get { return _totalUsePet; }
      set { _totalUsePet = value; }
    }
    private readonly global::System.Collections.Generic.List<MissionData> _missionOfRichang = new global::System.Collections.Generic.List<MissionData>();
    [global::ProtoBuf.ProtoMember(34, Name=@"missionOfRichang", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MissionData> missionOfRichang
    {
      get { return _missionOfRichang; }
    }
  
    private readonly global::System.Collections.Generic.List<MissionData> _missionOfChengjiu = new global::System.Collections.Generic.List<MissionData>();
    [global::ProtoBuf.ProtoMember(35, Name=@"missionOfChengjiu", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<MissionData> missionOfChengjiu
    {
      get { return _missionOfChengjiu; }
    }
  
    private readonly global::System.Collections.Generic.List<CarData> _carDatas = new global::System.Collections.Generic.List<CarData>();
    [global::ProtoBuf.ProtoMember(36, Name=@"carDatas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<CarData> carDatas
    {
      get { return _carDatas; }
    }
  
    private readonly global::System.Collections.Generic.List<RoleData> _roleDatas = new global::System.Collections.Generic.List<RoleData>();
    [global::ProtoBuf.ProtoMember(37, Name=@"roleDatas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<RoleData> roleDatas
    {
      get { return _roleDatas; }
    }
  
    private readonly global::System.Collections.Generic.List<PetData> _petDatas = new global::System.Collections.Generic.List<PetData>();
    [global::ProtoBuf.ProtoMember(38, Name=@"petDatas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<PetData> petDatas
    {
      get { return _petDatas; }
    }
  
    private readonly global::System.Collections.Generic.List<DepositData> _depositDatas = new global::System.Collections.Generic.List<DepositData>();
    [global::ProtoBuf.ProtoMember(39, Name=@"depositDatas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<DepositData> depositDatas
    {
      get { return _depositDatas; }
    }
  
    private readonly global::System.Collections.Generic.List<CostData> _costDatas = new global::System.Collections.Generic.List<CostData>();
    [global::ProtoBuf.ProtoMember(40, Name=@"costDatas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<CostData> costDatas
    {
      get { return _costDatas; }
    }
  
    private readonly global::System.Collections.Generic.List<FriendData> _friendDatas = new global::System.Collections.Generic.List<FriendData>();
    [global::ProtoBuf.ProtoMember(41, Name=@"friendDatas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<FriendData> friendDatas
    {
      get { return _friendDatas; }
    }
  
    private readonly global::System.Collections.Generic.List<RaceOfJingsu> _raceOfJingsus = new global::System.Collections.Generic.List<RaceOfJingsu>();
    [global::ProtoBuf.ProtoMember(42, Name=@"raceOfJingsus", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<RaceOfJingsu> raceOfJingsus
    {
      get { return _raceOfJingsus; }
    }
  
    private readonly global::System.Collections.Generic.List<RaceOfJixian> _raceOfJixians = new global::System.Collections.Generic.List<RaceOfJixian>();
    [global::ProtoBuf.ProtoMember(43, Name=@"raceOfJixians", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<RaceOfJixian> raceOfJixians
    {
      get { return _raceOfJixians; }
    }
  
    private readonly global::System.Collections.Generic.List<RaceOfPohuai> _raceOfPohuais = new global::System.Collections.Generic.List<RaceOfPohuai>();
    [global::ProtoBuf.ProtoMember(44, Name=@"raceOfPohuais", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<RaceOfPohuai> raceOfPohuais
    {
      get { return _raceOfPohuais; }
    }
  
    private readonly global::System.Collections.Generic.List<RaceOfTiaozhan> _raceOfTiaozhans = new global::System.Collections.Generic.List<RaceOfTiaozhan>();
    [global::ProtoBuf.ProtoMember(45, Name=@"raceOfTiaozhans", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<RaceOfTiaozhan> raceOfTiaozhans
    {
      get { return _raceOfTiaozhans; }
    }
  
    private long _dailyLoginRewardsTicks = default(long);
    [global::ProtoBuf.ProtoMember(46, IsRequired = false, Name=@"dailyLoginRewardsTicks", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long dailyLoginRewardsTicks
    {
      get { return _dailyLoginRewardsTicks; }
      set { _dailyLoginRewardsTicks = value; }
    }
    private int _dailyLoginRewardsCount = default(int);
    [global::ProtoBuf.ProtoMember(47, IsRequired = false, Name=@"dailyLoginRewardsCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int dailyLoginRewardsCount
    {
      get { return _dailyLoginRewardsCount; }
      set { _dailyLoginRewardsCount = value; }
    }
    private int _Feidanitem1Num = default(int);
    [global::ProtoBuf.ProtoMember(48, IsRequired = false, Name=@"Feidanitem1Num", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Feidanitem1Num
    {
      get { return _Feidanitem1Num; }
      set { _Feidanitem1Num = value; }
    }
    private int _Hudunitem2Num = default(int);
    [global::ProtoBuf.ProtoMember(49, IsRequired = false, Name=@"Hudunitem2Num", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Hudunitem2Num
    {
      get { return _Hudunitem2Num; }
      set { _Hudunitem2Num = value; }
    }
    private int _Yinshenitem3Num = default(int);
    [global::ProtoBuf.ProtoMember(50, IsRequired = false, Name=@"Yinshenitem3Num", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Yinshenitem3Num
    {
      get { return _Yinshenitem3Num; }
      set { _Yinshenitem3Num = value; }
    }
    private int _Jiasuitem4Num = default(int);
    [global::ProtoBuf.ProtoMember(51, IsRequired = false, Name=@"Jiasuitem4Num", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Jiasuitem4Num
    {
      get { return _Jiasuitem4Num; }
      set { _Jiasuitem4Num = value; }
    }
    private int _storyPassLevelMax = default(int);
    [global::ProtoBuf.ProtoMember(52, IsRequired = false, Name=@"storyPassLevelMax", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int storyPassLevelMax
    {
      get { return _storyPassLevelMax; }
      set { _storyPassLevelMax = value; }
    }
    private int _bgMusicMute = default(int);
    [global::ProtoBuf.ProtoMember(53, IsRequired = false, Name=@"bgMusicMute", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int bgMusicMute
    {
      get { return _bgMusicMute; }
      set { _bgMusicMute = value; }
    }
    private int _effectMute = default(int);
    [global::ProtoBuf.ProtoMember(54, IsRequired = false, Name=@"effectMute", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int effectMute
    {
      get { return _effectMute; }
      set { _effectMute = value; }
    }
    private string _missionOfPreviousJuqing = "";
    [global::ProtoBuf.ProtoMember(55, IsRequired = false, Name=@"missionOfPreviousJuqing", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string missionOfPreviousJuqing
    {
      get { return _missionOfPreviousJuqing; }
      set { _missionOfPreviousJuqing = value; }
    }
    private string _missionOfPreviousRelaxation = "";
    [global::ProtoBuf.ProtoMember(56, IsRequired = false, Name=@"missionOfPreviousRelaxation", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string missionOfPreviousRelaxation
    {
      get { return _missionOfPreviousRelaxation; }
      set { _missionOfPreviousRelaxation = value; }
    }
    private int _lotteryNum = default(int);
    [global::ProtoBuf.ProtoMember(57, IsRequired = false, Name=@"lotteryNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int lotteryNum
    {
      get { return _lotteryNum; }
      set { _lotteryNum = value; }
    }
    private long _historyMaxDistance = default(long);
    [global::ProtoBuf.ProtoMember(58, IsRequired = false, Name=@"historyMaxDistance", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long historyMaxDistance
    {
      get { return _historyMaxDistance; }
      set { _historyMaxDistance = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RequestLogin")]
  public partial class RequestLogin : global::ProtoBuf.IExtensible
  {
    public RequestLogin() {}
    
    private int _userID;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"userID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int userID
    {
      get { return _userID; }
      set { _userID = value; }
    }
    private string _userName;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"userName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string userName
    {
      get { return _userName; }
      set { _userName = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MissionData")]
  public partial class MissionData : global::ProtoBuf.IExtensible
  {
    public MissionData() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private int _state;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"state", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int state
    {
      get { return _state; }
      set { _state = value; }
    }
    private int _savePar = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"savePar", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int savePar
    {
      get { return _savePar; }
      set { _savePar = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CarData")]
  public partial class CarData : global::ProtoBuf.IExtensible
  {
    public CarData() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private int _accLv;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"accLv", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int accLv
    {
      get { return _accLv; }
      set { _accLv = value; }
    }
    private int _speedLv;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"speedLv", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int speedLv
    {
      get { return _speedLv; }
      set { _speedLv = value; }
    }
    private int _handlerLv;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"handlerLv", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int handlerLv
    {
      get { return _handlerLv; }
      set { _handlerLv = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RoleData")]
  public partial class RoleData : global::ProtoBuf.IExtensible
  {
    public RoleData() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private int _lv;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"lv", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int lv
    {
      get { return _lv; }
      set { _lv = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PetData")]
  public partial class PetData : global::ProtoBuf.IExtensible
  {
    public PetData() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"DepositData")]
  public partial class DepositData : global::ProtoBuf.IExtensible
  {
    public DepositData() {}
    
    private int _depositValue;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"depositValue", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int depositValue
    {
      get { return _depositValue; }
      set { _depositValue = value; }
    }
    private long _depositTime;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"depositTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long depositTime
    {
      get { return _depositTime; }
      set { _depositTime = value; }
    }
    private int _depositType;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"depositType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int depositType
    {
      get { return _depositType; }
      set { _depositType = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CostData")]
  public partial class CostData : global::ProtoBuf.IExtensible
  {
    public CostData() {}
    
    private int _costValue;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"costValue", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int costValue
    {
      get { return _costValue; }
      set { _costValue = value; }
    }
    private int _costType;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"costType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int costType
    {
      get { return _costType; }
      set { _costType = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"FriendData")]
  public partial class FriendData : global::ProtoBuf.IExtensible
  {
    public FriendData() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private bool _giveGift;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"giveGift", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool giveGift
    {
      get { return _giveGift; }
      set { _giveGift = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RaceOfJingsu")]
  public partial class RaceOfJingsu : global::ProtoBuf.IExtensible
  {
    public RaceOfJingsu() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private int _rank;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"rank", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int rank
    {
      get { return _rank; }
      set { _rank = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RaceOfJixian")]
  public partial class RaceOfJixian : global::ProtoBuf.IExtensible
  {
    public RaceOfJixian() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private int _distance;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"distance", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int distance
    {
      get { return _distance; }
      set { _distance = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RaceOfPohuai")]
  public partial class RaceOfPohuai : global::ProtoBuf.IExtensible
  {
    public RaceOfPohuai() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private int _score;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"score", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int score
    {
      get { return _score; }
      set { _score = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RaceOfTiaozhan")]
  public partial class RaceOfTiaozhan : global::ProtoBuf.IExtensible
  {
    public RaceOfTiaozhan() {}
    
    private string _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string id
    {
      get { return _id; }
      set { _id = value; }
    }
    private long _time;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"time", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long time
    {
      get { return _time; }
      set { _time = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}