using UnityEngine;

namespace QFramework.Car
{
	public partial class GameController : ViewController, IController, ISingleton
	{
        private GameModel m_gameModel;
        private PlayerModel m_playerModel;
        private EnemyModel m_enemyModel;
        private GameSystem m_gameSystem;

        public void OnSingletonInit()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public static GameController Instance
        {
            get { return MonoSingletonProperty<GameController>.Instance; }
        }

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

        public void Init()
        {
            m_gameModel = this.GetModel<GameModel>();
            m_playerModel = this.GetModel<PlayerModel>();
            m_enemyModel = this.GetModel<EnemyModel>();
            m_gameSystem = this.GetSystem<GameSystem>();

            m_enemyModel.OnEnemyDead.Register((position) =>
            {
                this.SendCommand(new DropExpCommand(position));
                this.SendCommand(new GetCoinCommand());
                this.SendCommand(new GetScoreCommand());
            }).UnRegisterWhenGameObjectDestroyed(gameObject);


            //UI Init
            UIKitExtension.OpenPanelAsync<UIBeginPanel>();
            UIKitExtension.OpenPanelAsync<UIAchievePop>(UILevel.PopUI);

            //Audio Init
            this.GetModel<SettingModel>().AudioEnable.RegisterWithInitValue((audioEnable) =>
            {
                AudioKit.Settings.IsMusicOn.Value = audioEnable;
                AudioKit.Settings.IsSoundOn.Value = audioEnable;
            });
            AudioKit.PlayMusic("Pretty Dungeon LOOP");

            //Player Init
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Init();
        }

        private void Update()
        {
#if UNITY_EDITOR

            //��ӡѪ��
            if (Input.GetKeyDown(KeyCode.Q))
            {
                print($"maxhp: {m_playerModel.MaxHp}   hp: {m_playerModel.Hp}");
            }

            //��ʼ��Ϸ
            if (Input.GetKeyDown(KeyCode.S))
            {
                m_gameSystem.GameStart();
            }

            //������Ϸ
            if (Input.GetKeyDown(KeyCode.D))
            {
                m_gameSystem.GameOver();
            }

            //��ͣ
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (m_gameModel.GameState == GameStates.isRunning)
                    this.GetSystem<GameSystem>().GamePause();
                else if (m_gameModel.GameState.Value == GameStates.isPaused)
                    this.GetSystem<GameSystem>().GameResume();
            }

            //����++
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                print("�����5");
                for(int i = 0; i < 5; i++)
                    this.SendCommand(new GetExpCommand());
            }

            //Ӳ��++
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                print("Ӳ�Ҽ�100");
                GameArch.Interface.GetModel<ItemModel>().Coin.Value += 100;
            }

#endif

        }



    }
}
