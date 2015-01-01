using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour {

	[SerializeField]
	public Vector3 startPoint;

	[SerializeField]
	public UnityChan.ThirdPersonCamera thirdPersonCamera;

	void Awake()
	{
		// Server接続
		PhotonNetwork.ConnectUsingSettings("v0.3");
	}
	// Lobby参加OK時
	void OnJoinedLobby()
	{
		// ランダムにRoom参加
		PhotonNetwork.JoinRandomRoom();
	}
	// Room参加NG時
	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Room参加失敗！");
		// 名前なしRoom作成
		PhotonNetwork.CreateRoom(null);
	}
	// Room参加OK時
	void OnJoinedRoom()
	{
		Debug.Log("Room参加成功！");
		//プレイヤーをインスタンス化
		Vector3 spawnPosition = new Vector3(
			Random.Range(startPoint.x -0.5f, startPoint.x + 0.5f), 
			2,
			Random.Range(startPoint.y - 0.5f, startPoint.z + 0.50f)); //生成位置
		var go = PhotonNetwork.Instantiate("UnitychanPrefab", spawnPosition, Quaternion.identity, 0);
		// カメラにプレイヤの情報を設定
		thirdPersonCamera.target = go;
		thirdPersonCamera.gameObject.SetActive(true);
	}
	// GUI表示
	void OnGUI()
	{
		// Photon接続状態
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
}