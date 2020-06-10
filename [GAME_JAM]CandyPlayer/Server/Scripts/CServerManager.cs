using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUserLength : byte
{
    Length = 2
}

public class CServerManager : CSingleton<CServerManager>
{
    public string m_ip;
    public ushort m_port;

    public CPlayerSession m_session;

    public Dictionary<uint, CSyncUser> m_userDic;

    public CSyncUser m_syncUserPrefabs;
    public CSyncUser m_syncBossPrefabs;

    public CPlayerController m_playerController;
    public CPlayerController m_playerPrefabs;
    public CBossController   m_bossPrefabs;

    public RandomCandy m_randCandySpawner;

    protected override void Awake()
    {
        base.Awake();

        if (m_session == null)
        {
            m_session = new CPlayerSession();

            m_session.Init();
            m_session.Connect(m_ip, m_port);
        }

        if (m_userDic == null)
        {
            m_userDic = new Dictionary<uint, CSyncUser>();
        }
    }

    private void FixedUpdate()
    {
        m_session.Update();
    }

    public void InitGameUser(CUserInfoListPacket _packet)
    {
        for (int i = 0; i < (int)EUserLength.Length; i++)
        {
            if (_packet.userInfoBUffer[i].m_userIndex == m_session.userIndex)
            {
                if (_packet.userInfoBUffer[i].m_userType == EUserType.User)
                    m_playerController = CPlayerController.Instantiate(m_playerPrefabs);
                else
                {
                    m_playerController = CBossController.Instantiate(m_bossPrefabs);
                //    m_randCandySpawner.BeginSpawn();
                }
                
                m_playerController.SetUserInfo(_packet.userInfoBUffer[i]);
                continue;
            }

            CSyncUser user;

            if (_packet.userInfoBUffer[i].m_userType == EUserType.User)
                user = CSyncUser.Instantiate(m_syncUserPrefabs);
            else
                user = CSyncBoss.Instantiate(m_syncBossPrefabs);

            user.SetUserInfo(_packet.userInfoBUffer[i]);
            m_userDic.Add(_packet.userInfoBUffer[i].m_userIndex, user);

            Debug.Log("Init");
        }
    }

    public void MoveUser(CUserPositionPacket _packet)
    {
        if (_packet.userIndex == m_session.userIndex) return;

        Debug.Log("Move" + _packet.userIndex);

        CSyncUser user = m_userDic[_packet.userIndex];
        user.BeginMove(_packet.position, _packet.direction);

    }

    public void MoveFinishUser(CUserPositionPacket _packet)
    {
        if (_packet.userIndex == m_session.userIndex) return;

        Debug.Log("MoveEnd" + _packet.userIndex);

        CSyncUser user = m_userDic[_packet.userIndex];
        user.EndMove(_packet.position, _packet.direction);

    }

    public void AttackBoss(CUserIndexPacket _packet)
    {
        if (_packet.userIndex == m_session.userIndex)
        {
            m_playerController.Attack();
            return;
        }

        CSyncUser user = m_userDic[_packet.userIndex];
        user.Attack();
    }

    public void SendData(byte[] _buffer)
    {
        if (!m_session.isSocketConnected())
        {
            Disconnect();
            return;
        }

        m_session.SendData(_buffer);
    }

    public void SendData(byte[] _buffer, int _size)
    {
        if (!m_session.isSocketConnected())
        {
            Disconnect();
            return;
        }
        m_session.SendData(_buffer, _size);
    }

    public void Disconnect()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        return;
    }
}
