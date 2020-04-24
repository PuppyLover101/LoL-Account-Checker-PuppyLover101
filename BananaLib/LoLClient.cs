using BananaLib.LoLTypes;
using BananaLib.RestService;
using BananaLib.RiotObjects.Exceptions;
using BananaLib.RiotObjects.Platform;
using BananaLib.RiotObjects.Team;
using RtmpSharp.IO;
using RtmpSharp.Messaging;
using RtmpSharp.Net;
using LolAccountChecker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;

namespace BananaLib
{
    public class LoLClient
    {
        private Region _region;
        private string _gameServer;
        private string _locale;
        private bool _useGarena;
        private Timer _heartbeatTimer;
        private DateTime _uptime;

        public Region Region
        {
            get
            {
                return this._region;
            }
            set
            {
                this._region = value;
                this._gameServer = value.GameServer();
                this._locale = value.Locale();
                this._useGarena = value.UseGarena();
            }
        }

        public string Username { get; set; }
        public long UserId { get; set; }

        public string Password { get; set; }

        public string ClientVersion { get; set; }

        public int LoginQueueTimeoutSeconds { get; set; }

        public int ConnectTimeoutSeconds { get; set; }

        public RtmpClient RtmpClient { get; private set; }

        public bool IsConnected { get; private set; }

        public Session Session { get; private set; }

        public string SessionToken
        {
            get
            {
                return this.Session != null ? this.Session.Token : null;
            }
        }

        public string AuthToken { get; private set; }

        public TimeSpan Uptime
        {
            get
            {
                return DateTime.Now - this._uptime;
            }
        }

        public int HeartbeatCount { get; private set; }

        private static SerializationContext SerializationContext { get; set; }

        public event OnConnectHandler OnConnect;

        public event OnDisconnectHandler OnDisconnect;

        public event OnLoginHandler OnLogin;

        public event OnErrorHandler OnError;

        public event EventHandler<MessageReceivedEventArgs> OnMessageReceived;

        public event StatusMessageUpdateHandler OnUpdateStatusMessage;

        public LoLClient()
        {
        }

        public LoLClient(string username, string password, Region region, string clientVersion)
        {
            this.Username = username;
            this.Password = password;
            this.Region = region;
            this.ClientVersion = clientVersion;
        }

        public Task<LoginDataPacket> GetLoginDataPacketForUser()
        {
            return InvokeAsync<LoginDataPacket>("clientFacadeService", "getLoginDataPacketForUser");
        }

        public Task<object> SelectChampion(int championId)
        {
            return InvokeAsync<object>("gameService", "selectChampion", (object)championId);
        }

        public Task<object> SelectChampionSkin(int championId, int skinId)
        {
            return InvokeAsync<object>("gameService", "selectChampionSkin", (object)championId, (object)skinId);
        }

        public Task<object> ChampionSelectCompleted()
        {
            return InvokeAsync<object>("gameService", "championSelectCompleted");
        }

        public Task<PlatformGameLifecycleDTO> RetrieveInProgressSpectatorGameInfo(string summonerName)
        {
            return InvokeAsync<PlatformGameLifecycleDTO>("gameService", "retrieveInProgressSpectatorGameInfo", (object)summonerName);
        }

        public Task<object> AcceptPoppedGame(bool acceptGame)
        {
            return InvokeAsync<object>("gameService", "acceptPoppedGame", (object)acceptGame);
        }

        public Task<object> BanUserFromGame(double gameId, double accountId)
        {
            return InvokeAsync<object>("gameService", "banUserFromGame", (object)gameId, (object)accountId);
        }

        public Task<object> BanObserverFromGame(double gameId, double accountId)
        {
            return InvokeAsync<object>("gameService", "banObserverFromGame", (object)gameId, (object)accountId);
        }

        public Task<object> BanChampion(int championId)
        {
            return InvokeAsync<object>("gameService", "banChampion", (object)championId);
        }

        public Task<ChampionBanInfoDTO[]> GetChampionsForBan()
        {
            return InvokeAsync<ChampionBanInfoDTO[]>("gameService", "getChampionsForBan");
        }

        public Task<object> RemoveBotChampion(int champId, BotParticipant item)
        {
            return InvokeAsync<object>("gameService", "removeBotChampion", (object)champId, (object)item);
        }

        public Task<object> SelectBotChampion(int champId, BotParticipant item)
        {
            return InvokeAsync<object>("gameService", "selectBotChampion", (object)champId, (object)item);
        }

        public Task<PracticeGameSearchResult[]> ListAllPracticeGames()
        {
            return InvokeAsync<PracticeGameSearchResult[]>("gameService", "listAllPracticeGames");
        }

        public Task<object> JoinGame(double gameId)
        {
            return InvokeAsync<object>("gameService", "joinGame", (object)gameId, null);
        }

        public Task<object> JoinGame(double gameId, string password)
        {
            return InvokeAsync<object>("gameService", "joinGame", (object)gameId, (object)password);
        }

        public Task<object> ObserveGame(double gameId)
        {
            return InvokeAsync<object>("gameService", "observeGame", (object)gameId, null);
        }

        public Task<object> ObserveGame(double gameId, string password)
        {
            return InvokeAsync<object>("gameService", "observeGame", (object)gameId, (object)password);
        }

        public Task<bool> SwitchTeams(double gameId)
        {
            return InvokeAsync<bool>("gameService", "switchTeams", (object)gameId);
        }

        public Task<bool> SwitchPlayerToObserver(double gameId)
        {
            return this.InvokeAsync<bool>("gameService", "switchPlayerToObserver", (object)gameId);
        }

        public Task<bool> SwitchObserverToPlayer(double gameId, int team)
        {
            return this.InvokeAsync<bool>("gameService", "switchObserverToPlayer", (object)gameId, (object)team);
        }

        public Task<object> QuitGame()
        {
            return this.InvokeAsync<object>("gameService", "quitGame");
        }

        public Task<GameDTO> CreatePracticeGame(PracticeGameConfig config)
        {
            return this.InvokeAsync<GameDTO>("gameService", "createPracticeGame", (object)config);
        }

        public Task<StartChampSelectDTO> StartChampionSelection(double gameId, double optomisticLock)
        {
            return this.InvokeAsync<StartChampSelectDTO>("gameService", "startChampionSelection", (object)gameId, (object)optomisticLock);
        }

        public Task<object> SetClientReceivedGameMessage(double gameId, string argument)
        {
            return this.InvokeAsync<object>("gameService", "setClientReceivedGameMessage", (object)gameId, (object)argument);
        }

        public Task<GameDTO> GetLatestGameTimerState(double gameId, string gameState, int pickTurn)
        {
            return this.InvokeAsync<GameDTO>("gameService", "getLatestGameTimerState", (object)gameId, (object)gameState, (object)pickTurn);
        }

        public Task<object> SelectSpells(int spellOneId, int spellTwoId)
        {
            return this.InvokeAsync<object>("gameService", "selectSpells", (object)spellOneId, (object)spellTwoId);
        }

        public Task<SummonerActiveBoostsDTO> GetSummonerActiveBoosts()
        {
            return this.InvokeAsync<SummonerActiveBoostsDTO>("inventoryService", "getSumonerActiveBoosts");
        }

        public Task<ChampionDTO[]> GetAvailableChampions()
        {
            return this.InvokeAsync<ChampionDTO[]>("inventoryService", "getAvailableChampions");
        }

        private Task<T> LcdsServiceProxyCall<T>(string service, string method, params object[] parameters)
        {
            return this.InvokeAsync<T>("lcdsServiceProxy", "call", (object)Uuid.NewUuid(), (object)service, (object)method, (object)parameters);
        }

        public Task<object> getReformCard()
        {
            return this.LcdsServiceProxyCall<object>("toxicfeedback.server", "getReformCard", (object[])null);
        }

        public Task<object> getChampionMasteryScore(double summonerId)
        {
            return this.InvokeAsync<object>("championMastery", "getChampionMasteryScore", (object)summonerId);
        }

        public Task<SummonerLeagueItemsDTO> getMyLeaguePositions()
        {
            return this.LcdsServiceProxyCall<SummonerLeagueItemsDTO>("leagues", "getMyLeaguePositions", (object[])null);
        }

        public Task<SummonerLeaguesDTO> getAllMyLeagues()
        {
            return this.LcdsServiceProxyCall<SummonerLeaguesDTO>("leagues", "getAllMyLeagues", (object[])null);
        }

        public Task<SummonerLeaguesDTO> getChallengerLeague(string queueType)
        {
            return this.LcdsServiceProxyCall<SummonerLeaguesDTO>("leagues", "getChallengerLeague", (object)queueType);
        }

        public Task<SummonerLeaguesDTO> getMasterLeagueTopX(string queueType, int topX)
        {
            return this.LcdsServiceProxyCall<SummonerLeaguesDTO>("leagues", "getMasterLeagueTopX", (object)queueType, (object)topX);
        }

        public Task<Session> Login(AuthenticationCredentials credentials)
        {
            return this.InvokeAsync<Session>("loginService", "login", (object)credentials);
        }

        public Task<string> Login(string authToken)
        {
            return this.InvokeAsync<string>("auth", "8", (object)authToken);
        }

        public Task<string> Logout(string sessionToken)
        {
            return this.InvokeAsync<string>("loginService", "logout", (object)sessionToken);
        }

        public Task<string> PerformLcdsHeartBeat(int accountId, string sessionToken, int heartbeatCount)
        {
            return this.InvokeAsync<string>("loginService", "performLCDSHeartBeat", (object)accountId, (object)sessionToken, (object)heartbeatCount, (object)DateTime.Now.ToString("ddd MMM d yyyy HH:mm:ss 'GMT-0700'"));
        }

        public Task<string> GetStoreUrl()
        {
            return this.InvokeAsync<string>("loginService", "getStoreUrl");
        }

        public Task<MasteryBookDTO> GetMasteryBook(double summonerId)
        {
            return this.InvokeAsync<MasteryBookDTO>("masteryBookService", "getMasteryBook", (object)summonerId);
        }

        public Task<MasteryBookDTO> SaveMasteryBook(MasteryBookDTO masteryBookPage)
        {
            return this.InvokeAsync<MasteryBookDTO>("masteryBookService", "saveMasteryBook", (object)masteryBookPage);
        }

        public Task<GameQueueConfig[]> GetAvailableQueues()
        {
            return this.InvokeAsync<GameQueueConfig[]>("matchmakerService", "getAvailableQueues");
        }

        public Task<SearchingForMatchNotification> AttachToQueue(MatchMakerParams matchMakerParams)
        {
            return this.InvokeAsync<SearchingForMatchNotification>("matchmakerService", "attachToQueue", (object)matchMakerParams);
        }

        public Task<SearchingForMatchNotification> AttachToQueue(MatchMakerParams matchMakerParams, AsObject token)
        {
            return this.InvokeAsync<SearchingForMatchNotification>("matchmakerService", "attachToQueue", (object)matchMakerParams, (object)token);
        }

        public Task<bool> CancelFromQueueIfPossible(double summonerId)
        {
            return this.InvokeAsync<bool>("matchmakerService", "cancelFromQueueIfPossible", (object)summonerId);
        }
        //TEST///////////////////////////////////////////////
        public Task<object> CreateGroupFinderLobby(double gameMode, string uuid)
        {
            return this.InvokeAsync<object>("lcdsGameInvitationService", "createGroupFinderLobby", gameMode, uuid);
        }
        public Task<object> CreateArrangedTeamLobby(double gameMode)
        {
            return this.InvokeAsync<object>("lcdsGameInvitationService", "createArrangedTeamLobby", gameMode);
        }
        public Task<object> CreateArrangedBotTeamLobby(double gameMode, string difficulty)
        {
            return this.InvokeAsync<object>("lcdsGameInvitationService", "createArrangedBotTeamLobby", new object[] { gameMode, difficulty });
        }
        public Task<object> AcceptLobbyInvite(string invitationId)
        {
            return this.InvokeAsync<object>("lcdsGameInvitationService", "accept", invitationId);
        }
        public Task<object> DeclineLobbyInvite(string invitationId)
        {
            return this.InvokeAsync<object>("lcdsGameInvitationService", "decline", invitationId);
        }
        public Task<object> InvitePlayer(double summonerId)
        {
            return this.InvokeAsync<object>("lcdsGameInvitationService", "invite", summonerId);
        }

        public Task<object> DestroyGroupFinderLobby()
        {
            return this.InvokeAsync<object>("lcdsGameInvitationService", "destroyGroupFinderLobby");
        }

        public Task<object> LeaveGroupFinderLobby()
        {
            return this.InvokeAsync<object>("lcdsGameInvitationService", "leave");
        }

        ////////////////////////////////////////////////

        public Task<object> AcceptInviteForMatchmakingGame(string inviteId)
        {
            return this.InvokeAsync<object>("matchmakerService", "acceptInviteForMatchmakingGame", (object)inviteId);
        }

        public Task<SearchingForMatchNotification> AttachTeamToQueue(MatchMakerParams matchMakerParams)
        {
            return this.InvokeAsync<SearchingForMatchNotification>("matchmakerService", "attachTeamToQueue", (object)matchMakerParams);
        }

        public Task<SearchingForMatchNotification> AttachTeamToQueue(MatchMakerParams matchMakerParams, AsObject token)
        {
            return this.InvokeAsync<SearchingForMatchNotification>("matchmakerService", "attachTeamToQueue", (object)matchMakerParams, (object)token);
        }

        public Task<QueueInfo> GetQueueInformation(double queueId)
        {
            return this.InvokeAsync<QueueInfo>("matchmakerService", "getQueueInfo", (object)queueId);
        }

        public Task<object> PurgeFromQueues()
        {
            return this.InvokeAsync<object>("matchmakerService", "purgeFromQueues");
        }

        public Task<object> LeaveLeaverBuster(string access)
        {
            return this.InvokeAsync<object>("matchmakerService", "purgeFromQueues", (object)access);
        }

        public Task<object> ProcessEloQuestionaire(PlayerSkill playerSkill)
        {
            return this.InvokeAsync<object>("playerStatsService", "processEloQuestionaire", (object)playerSkill.ToString());
        }

        public Task<PlayerLifetimeStats> RetrievePlayerStatsByAccountId(double accountId, int season)
        {
            return this.InvokeAsync<PlayerLifetimeStats>("playerStatsService", "retrievePlayerStatsByAccountId", (object)accountId, (object)season);
        }

        public Task<ChampionStatInfo[]> RetrieveTopPlayedChampions(double accountId, string gameMode)
        {
            return this.InvokeAsync<ChampionStatInfo[]>("playerStatsService", "retrieveTopPlayedChampions", (object)accountId, (object)gameMode);
        }

        public Task<AggregatedStats> GetAggregatedStats(double summonerId, string gameMode, string season)
        {
            return this.InvokeAsync<AggregatedStats>("playerStatsService", "getAggregatedStats", (object)summonerId, (object)gameMode, (object)season);
        }

        public Task<RecentGames> GetRecentGames(double accountId)
        {
            return this.InvokeAsync<RecentGames>("playerStatsService", "getRecentGames", (object)accountId);
        }

        public Task<TeamAggregatedStatsDTO[]> GetTeamAggregatedStats(TeamId teamId)
        {
            return this.InvokeAsync<TeamAggregatedStatsDTO[]>("playerStatsService", "getTeamAggregatedStats", (object)teamId);
        }

        public Task<EndOfGameStats> GetTeamEndOfGameStats(TeamId teamId, double gameId)
        {
            return this.InvokeAsync<EndOfGameStats>("playerStatsService", "getTeamEndOfGameStats", (object)teamId, (object)gameId);
        }

        public Task<T> InvokeAsync<T>(string destination, string method, params object[] arguments)
        {
            try
            {
                return this.RtmpClient.InvokeAsync<T>("my-rtmps", destination, method, arguments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return default(Task<T>);
        }

        public static SerializationContext GetSerializationContext()
        {
            string[] strArray = new string[4]
            {
                "BananaLib.RiotObjects.Kudos",
                "BananaLib.RiotObjects.Platform",
                "BananaLib.RiotObjects.Leagues",
                "BananaLib.RiotObjects.Exceptions"
            };
            SerializationContext serializationContext = new SerializationContext();
            try
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                foreach (string str in strArray)
                {
                    string nspace = str;
                    foreach (Type type in ((IEnumerable<Type>)executingAssembly.GetTypes()).Where<Type>((Func<Type, bool>)(x =>
                    {
                        if (x.Namespace != null)
                            return x.Namespace.Equals(nspace, StringComparison.Ordinal);
                        return false;
                    })))
                    {
                        serializationContext.Register(type);
                        SerializedNameAttribute[] customAttributes = (SerializedNameAttribute[])type.GetCustomAttributes<SerializedNameAttribute>(false);
                        if (customAttributes.Length != 0)
                            serializationContext.RegisterAlias(type, customAttributes[0].SerializedName, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return serializationContext;
        }

        public Task<SpellBookDTO> GetSpellBook(double summonerId)
        {
            return this.InvokeAsync<SpellBookDTO>("spellBookService", "getSpellBook", (object)summonerId);
        }

        public Task<SpellBookPageDTO> SelectDefaultSpellBookPage(SpellBookPageDTO spellbookPage)
        {
            return this.InvokeAsync<SpellBookPageDTO>("spellBookService", "selectDefaultSpellBookPage", (object)spellbookPage);
        }

        public Task<SpellBookDTO> SaveSpellBook(SpellBookDTO spellbook)
        {
            return this.InvokeAsync<SpellBookDTO>("spellBookService", "saveSpellBook", (object)spellbook);
        }

        public Task<AllSummonerData> GetAllSummonerDataByAccount(double accountId)
        {
            return this.InvokeAsync<AllSummonerData>("summonerService", "getAllSummonerDataByAccount", (object)accountId);
        }

        public Task<PublicSummoner> GetSummonerByName(string summonerName)
        {
            return this.InvokeAsync<PublicSummoner>("summonerService", "getSummonerByName", (object)summonerName);
        }

        public Task<AllPublicSummonerDataDTO> GetAllPublicSummonerDataByAccount(double accountId)
        {
            return this.InvokeAsync<AllPublicSummonerDataDTO>("summonerService", "getAllPublicSummonerDataByAccount", (object)accountId);
        }

        public Task<string> GetSummonerInternalNameByName(string summonerName)
        {
            return InvokeAsync<string>("summonerService", "getSummonerInternalNameByName", (object)summonerName);
        }

        public Task<object> UpdateProfileIconId(int iconId)
        {
            return InvokeAsync<object>("summonerService", "updateProfileIconId", (object)iconId);
        }

        public Task<string[]> GetSummonerNames(double[] summonerIds)
        {
            return InvokeAsync<string[]>("summonerService", "getSummonerNames", (object)summonerIds);
        }

        public Task<AllSummonerData> CreateDefaultSummoner(string playerName)
        {
            return InvokeAsync<AllSummonerData>("summonerService", "createDefaultSummoner", (object)playerName);
        }

        public Task<PlayerDto> CreatePlayer()
        {
            return InvokeAsync<PlayerDto>("summonerTeamService", "createPlayer");
        }

        public Task<TeamDto> FindTeamById(TeamId teamId)
        {
            return InvokeAsync<TeamDto>("summonerTeamService", "findTeamById", (object)teamId);
        }

        public Task<TeamDto> FindTeamByName(string teamName)
        {
            return InvokeAsync<TeamDto>("summonerTeamService", "findTeamByName", (object)teamName);
        }

        public Task<object> DisbandTeam(TeamId teamId)
        {
            return InvokeAsync<object>("summonerTeamService", "disbandTeam", (object)teamId);
        }

        public Task<bool> IsTeamNameValidAndAvailable(string teamName)
        {
            return InvokeAsync<bool>("summonerTeamService", "isNameValidAndAvailable", (object)teamName);
        }

        public Task<bool> IsTeamTagValidAndAvailable(string tagName)
        {
            return InvokeAsync<bool>("summonerTeamService", "isTagValidAndAvailable", (object)tagName);
        }

        public Task<TeamDto> CreateTeam(string teamName, string tagName)
        {
            return InvokeAsync<TeamDto>("summonerTeamService", "createTeam", (object)teamName, (object)tagName);
        }

        public Task<TeamDto> TeamInvitePlayer(double summonerId, TeamId teamId)
        {
            return InvokeAsync<TeamDto>("summonerTeamService", "invitePlayer", (object)summonerId, (object)teamId);
        }

        public Task<TeamDto> KickPlayer(double summonerId, TeamId teamId)
        {
            return InvokeAsync<TeamDto>("summonerTeamService", "kickPlayer", (object)summonerId, (object)teamId);
        }

        public Task<PlayerDto> FindPlayer(double summonerId)
        {
            return InvokeAsync<PlayerDto>("summonerTeamService", "findPlayer", (object)summonerId);
        }

        public Task<object> DeclineTeamInvite(TeamId teamId)
        {
            return InvokeAsync<object>("summonerTeamService", "leaveTeam", (object)teamId);
        }

        public Task<object> AcceptTeamInvite(TeamId teamId)
        {
            return InvokeAsync<object>("summonerTeamService", "joinTeam", (object)teamId);
        }

        public Task<string> GetAccountState()
        {
            return InvokeAsync<string>("accountService", "getAccountStateForCurrentSession");
        }

        public Task<SummonerRuneInventory> GetSummonerRuneInventory(double summonerId)
        {
            return InvokeAsync<SummonerRuneInventory>("summonerRuneService", "getSummonerRuneInventory", (object)summonerId);
        }

        public Task<SummonerIconInventoryDTO> GetSummonerIconInventory(double summonerId)
        {
            return InvokeAsync<SummonerIconInventoryDTO>("summonerIconService", "getSummonerIconInventory", (object)summonerId);
        }

        public static IEnumerable<Region> GetAllAvailableRegions()
        {
            return Enum.GetValues(typeof(Region)).Cast<Region>();
        }

        private void SetDefaultsParams()
        {
            if (SerializationContext == null)
                SerializationContext = GetSerializationContext();
            if (ConnectTimeoutSeconds > 0)
                return;
            ConnectTimeoutSeconds = 60;
        }

        public async Task<bool> ConnectAndLogin()
        {
            if (_useGarena)
            {
                //this.RaiseError("Garena servers are not yet supported.", ErrorType.Connect);
                //return false;
            }
            SetDefaultsParams();
            RtmpClient = new RtmpClient(new Uri(_gameServer), SerializationContext, ObjectEncoding.Amf3);
            // ISSUE: reference to a compiler-generated field
            RtmpClient.MessageReceived += OnMessageReceived;
            LoginQueue loginQueue = new LoginQueue(Username, Password, Region);
            loginQueue.OnAuthFailed += message => RaiseError(message, ErrorType.Login);
            loginQueue.OnUpdateStatusMessage += (sender, s) =>
            {
                if (OnUpdateStatusMessage == null)
                    return;
                object sender1 = sender;
                string e = s;
                OnUpdateStatusMessage(sender1, e);
            };
            if (!await loginQueue.GetAuthToken().ConfigureAwait(false))
                return false;
            this.AuthToken = loginQueue.AuthToken;
            this.UserId = loginQueue.UserId;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                while (sw.ElapsedMilliseconds / 1000L <= ConnectTimeoutSeconds)
                {
                    try
                    {
                        AsObject asObject = await RtmpClient.ConnectAsync().ConfigureAwait(false);
                        sw.Stop();
                        goto label_13;
                    }
                    catch (Exception ex)
                    {
                        Tools.Log(ex.StackTrace);
                    }
                    await Task.Delay(5000).ConfigureAwait(false);
                    continue;
                    label_13:

                    OnConnect?.Invoke((object)this, EventArgs.Empty);
                    this.RtmpClient.SetChunkSize(int.MaxValue);

                    AuthenticationCredentials cred = new AuthenticationCredentials()
                    {
                        Username = Username,
                        Password = Password,
                        ClientVersion = ClientVersion,
                        Domain = "lolclient.lol.riotgames.com",
                        Locale = _locale,
                        OperatingSystem = "Windows 10",
                        AuthToken = AuthToken
                    };

                    if (_useGarena)
                    {
                        cred.PartnerCredentials = loginQueue.Token;
                        cred.Username = loginQueue.User;//UserId
                        cred.Password = null;
                    }

                    this.Session = await this.Login(cred).ConfigureAwait(false);
                    string[] strArray = new string[3]
                    {
                        "gn",
                        "cn",
                        "bc"
                    };

                    for (int index = 0; index < strArray.Length; ++index)
                        strArray[index] = string.Format("{0}-{1}", strArray[index], Session.AccountSummary.AccountId);

                    bool[] flagArray = await Task.WhenAll(new Task<bool>[3]
                    {
                        RtmpClient.SubscribeAsync("my-rtmps", "messagingDestination", strArray[0], strArray[0]),
                        RtmpClient.SubscribeAsync("my-rtmps", "messagingDestination", strArray[1], strArray[1]),
                        RtmpClient.SubscribeAsync("my-rtmps", "messagingDestination", "bc", strArray[2])
                    }).ConfigureAwait(false);


                    if (_useGarena)
                        IsConnected = await RtmpClient.LoginAsync(cred.Username, Session.Token).ConfigureAwait(false);
                    else
                        IsConnected = await RtmpClient.LoginAsync(Username.ToLower(), Session.Token).ConfigureAwait(false);

                    //IsConnected = await RtmpClient.LoginAsync(Username.ToLower(), Session.Token).ConfigureAwait(false);

                    _heartbeatTimer = new Timer();
                    _heartbeatTimer.Elapsed += new ElapsedEventHandler(DoHeartBeat);
                    _heartbeatTimer.Interval = 120000.0;
                    _heartbeatTimer.Start();

                    _uptime = DateTime.Now;

                    OnLogin?.Invoke(Username);
                    return true;
                }
                sw.Stop();
                RaiseError("Connection failed.", ErrorType.Connect);
                return false;
            }
            catch (InvocationException ex)
            {
                if (ex.RootCause != null && ex.RootCause.GetType() == typeof(LoginFailedException))
                {
                    LoginFailedException rootCause = (LoginFailedException)ex.RootCause;
                    string errorCode = rootCause.ErrorCode;
                    if (!(errorCode == "LOGIN-0018"))
                    {
                        if (errorCode == "LOGIN-0019")
                        {
                            RaiseError("User blocked.", ErrorType.Login);
                            return false;
                        }
                        this.RaiseError(string.Format("{0}: {1}", "LoginFailedException", rootCause.ErrorCode).Trim(), ErrorType.Login);
                        return false;
                    }
                    RaiseError("Password change required.", ErrorType.Password);
                    return false;
                }
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    RaiseError(ex.Message, ErrorType.Invoke);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Tools.Log(ex.StackTrace);
            }
            finally
            {
                sw.Stop();
            }
            this.RaiseError("Login failed with unknown error. Try again later.", ErrorType.Login);
            return false;
        }

        private async void DoHeartBeat(object sender, ElapsedEventArgs e)
        {
            if (!IsConnected)
                return;
            try
            {
                string str = await PerformLcdsHeartBeat(Convert.ToInt32(Session.AccountSummary.AccountId), Session.Token, HeartbeatCount).ConfigureAwait(false);
                HeartbeatCount = HeartbeatCount + 1;
            }
            catch
            {
            }
        }

        public async void Disconnect()
        {
            try
            {
                string str = await Logout(SessionToken).ConfigureAwait(false);
            }
            catch
            {
            }
            if (!RtmpClient.IsDisconnected)
            {
                try
                {
                    RtmpClient.Close();
                }
                catch(Exception ex)
                {
                    Tools.Log(ex.Message);
                    Tools.Log(ex.StackTrace);
                }
            }
            HeartbeatCount = 0;
            Timer heartbeatTimer = _heartbeatTimer;
            if (heartbeatTimer != null)
                heartbeatTimer.Stop();
            IsConnected = false;

            OnDisconnect?.Invoke(this, EventArgs.Empty);
        }

        public static string GetLoLIpAddress()
        {
            try
            {
                WebRequest webRequest = WebRequest.Create("http://ll.leagueoflegends.com/services/connection_info");
                WebResponse response = webRequest.GetResponse();
                StringBuilder stringBuilder = new StringBuilder();
                Stream responseStream = response.GetResponseStream();
                int num;
                while (responseStream != null && (num = responseStream.ReadByte()) != -1)
                    stringBuilder.Append((char)num);
                webRequest.Abort();
                return new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(stringBuilder.ToString())["ip_address"];
            }
            catch
            {
                return null;
            }
        }

        private void RaiseError(string message, ErrorType type)
        {
            OnError?.Invoke(new Error()
            {
                Type = type,
                Message = message
            });
        }

        public delegate void OnConnectHandler(object sender, EventArgs e);

        public delegate void OnDisconnectHandler(object sender, EventArgs e);

        public delegate void OnLoginHandler(string username);

        public delegate void OnErrorHandler(Error error);

        public delegate void StatusMessageUpdateHandler(object sender, string e);
    }
}