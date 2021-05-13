using UnityEngine;
using System.Collections;
public class ShowFPS : MonoBehaviour
{
	private float m_LastUpdateShowTime = 0f;  // le temp de derni¨¨re mise ¨¤ jour de la fr¨¦quence 
	private float m_UpdateShowDeltaTime = 0.01f;//Intervalle de temps pour la mise ¨¤ jour de la fr¨¦quence 
	private int m_FrameUpdate = 0;//fr¨¦quence
	private float m_FPS = 0;
	// pour inisialiser
	void Start()
	{
		m_LastUpdateShowTime = Time.realtimeSinceStartup;
	}
	// Update par chaque fr¨¦quence
	void Update()
	{
		m_FrameUpdate++;
		if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
		{
			m_FPS = m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime);
			m_FrameUpdate = 0;
			m_LastUpdateShowTime = Time.realtimeSinceStartup;
		}
	}
	void OnGUI()
	{
		GUI.Label(new Rect(10, 0, 100, 100), "FPS: " + m_FPS);
	}

}