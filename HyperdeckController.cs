using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.IO;


public class HyperdeckController : MonoBehaviour {

	TcpClient mySocket;
	internal Boolean socketReady = false;

	NetworkStream theStream;
	StreamWriter theWriter;
	StreamReader theReader;

	public String Host = "192.168.1.80";
	Int32 Port = 9993;

	// Use this for initialization
	void Start () {
		if (!socketReady) {
			setupSocket ();
			writeSocket ("serverStatus:");
		}
	}

	// Update is called once per frame
	void Update () {
		string receivedText = readSocket ();
		if(receivedText!="")
			print (receivedText);
	}

	public void Record(){
		writeSocket ("record");
	}

	public void Stop(){
		writeSocket ("stop");
	}

	public void Play(){
		writeSocket ("play");
	}

	public void PreviewMode(bool isActive_){
		string isActive;
		isActive = isActive_.ToString ();
		writeSocket ("preview: enable: " + isActive);
	}

	public void Help(){
		writeSocket ("help");
	}

	public void DeviceInfo(){
		writeSocket ("device info");
	}

	public void DiskList(){
		writeSocket ("disk list");
	}

	public void SlotInfo(){
		writeSocket ("slot info");
	}

	public void SlotInfoId1(){
		writeSocket ("slot info: slot id: 1");
	}

	public void SlotInfoId2(){
		writeSocket ("slot info: slot id: 2");
	}

	public void SlotSelectId1(){
		writeSocket ("slot select: slot id: 1");
	}

	public void SlotSelectId2(){
		writeSocket ("slot select: slot id: 2");
	}

	public void NotifyStatus(){
		writeSocket ("notify");
	}

	public void ActivateNotify(bool isActive_){
		string isActive;
		isActive = isActive_.ToString ();
		writeSocket ("notify: remote: " + isActive);
		writeSocket ("notify: transport: " + isActive);
		writeSocket ("notify: slot: " + isActive);
		writeSocket ("notify: configuration: " + isActive);
	}

	void OnApplicationQuit(){
		closeSocket ();
	}

	public void setupSocket(){
		try
		{
			mySocket = new TcpClient(Host, Port);
			theStream = mySocket.GetStream();
			theWriter = new StreamWriter(theStream);
			theReader = new StreamReader(theStream);
			socketReady = true;
		}

		catch (Exception e) 
		{
			Debug.Log ("Socket error: " + e);
		}
	}

	public void writeSocket(string theLine){
		if (!socketReady)
			return;
		String foo = theLine + "\r\n";
		theWriter.Write (foo);
		theWriter.Flush ();
	}

	public String readSocket(){
		if (!socketReady)
			return "";

		if (theStream.DataAvailable) {
			return theReader.ReadLine ();
		}

		return "";
	}

	public void closeSocket(){
		if (!socketReady)
			return;

		theWriter.Close ();
		theReader.Close ();
		mySocket.Close ();

		socketReady = false;
	}
}
