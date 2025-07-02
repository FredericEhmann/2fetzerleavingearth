using LiteNetLib;
using LiteNetLib.Utils;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetworkClient : MonoBehaviour, INetEventListener
{
    private NetManager _netManager;
    private NetPeer _server;
    private NetDataWriter _writer;
    public static int latency;
    public static NetworkClient _instance;
    public static NetworkClient Instance {
        get { return _instance; }
    }
    private void Awake()
    {
        if (_instance != null) {
            Destroy(gameObject);
        }else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    private void Init() {
        _writer = new NetDataWriter();
        _netManager = new NetManager(this)
        { 
            DisconnectTimeout=10000
        };
        _netManager.Start();
    }

    public void Connect() {
        _netManager.Connect("localhost", 9050, "");
        Debug.Log("Connecting to localhost 9050");
    }

    public void SendServer(string data) {
        var bytes = Encoding.UTF8.GetBytes(data);
        _server.Send(bytes, DeliveryMethod.ReliableOrdered);
    }

    // Update is called once per frame
    void Update()
    {
        _netManager.PollEvents();
    }

    void INetEventListener.OnConnectionRequest(ConnectionRequest request)
    {
        Debug.Log("OnConnectionRequest: " + request);
    }

    void INetEventListener.OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {
        Debug.Log("OnNetworkError: " + endPoint + " socketError: " + socketError);
    }

    void INetEventListener.OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {
        NetworkClient.latency = latency;
    }

    void INetEventListener.OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
    {
        var data = Encoding.UTF8.GetString(reader.RawData).Replace("\0","");
        Debug.Log($"Data received from Server: '{data}'");
    }

    void INetEventListener.OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        Debug.Log("OnNetworkReceiveUnconnected remoteEndPoint: " + remoteEndPoint+" reader: "+ reader+ " messageType: "+messageType);
    }

    void INetEventListener.OnPeerConnected(NetPeer peer)
    {
        Debug.Log("We connected to Server at + "+peer.Address);
        _server = peer;
        Constants.connected = true;
    }

    void INetEventListener.OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Debug.Log("Lost Connection to Server!");
        Constants.connected = false;
    }

}
