// Credit: https://www.patreon.com/posts/grass-geometry-1-40090373

using UnityEngine;
 
public class ShaderInteractor : MonoBehaviour
{
    private void Update()
    {
        // Set player position
        Shader.SetGlobalVector("_PositionMoving", transform.position);
        
        // Set player movement speed if you can have the value
        // When the value is greater than zero, surround grass 
        // will be highlighted. Set it 0 to ignore this effect!
       // Shader.SetGlobalFloat("_MovingSpeedPercent", 0);
    }
}