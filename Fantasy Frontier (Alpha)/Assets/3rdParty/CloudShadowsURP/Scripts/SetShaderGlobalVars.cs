using UnityEngine;
using System.Collections;

    public class SetShaderGlobalVars : MonoBehaviour
    {

		public Texture2D shadowTexture;
		public Texture2D noiseTexture;
		public Vector4 shadowTextureTileOffset = new Vector4(0.1f,0.1f,0.0f,0.0f);
		public Vector4 noiseTextureTileOffset = new Vector4(0.1f,0.1f,0.0f,0.0f);
		public Color shadowColor = new Color(0.0f,0.0f,0.0f,0.75f);
		public float speedX = 1.0f;
		public float speedY = -1.0f;
		public float offset;
		
        void Awake()
        {
			Shader.SetGlobalTexture("_ShadowTex", shadowTexture);
			Shader.SetGlobalTexture("_NoiseTex", noiseTexture);
			Shader.SetGlobalVector("_ShadowTexTO", shadowTextureTileOffset);
			Shader.SetGlobalVector("_NoiseTexTO", noiseTextureTileOffset);
			Shader.SetGlobalColor("_CloudsColor", shadowColor);
			Shader.SetGlobalFloat("_SpeedX", speedX);
			Shader.SetGlobalFloat("_SpeedY", speedY);
			Shader.SetGlobalFloat("_Offset", offset);
        }
    }

