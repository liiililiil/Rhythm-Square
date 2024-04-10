using UnityEngine;

public class SkinMaster : MonoBehaviour
{   
    public int PlayerSelect;
    public int TileSelect;
    public Sprite[] Player;
    public Sprite[] Tile;

    public void Update(){
        if(PlayerSelect > Player.Length){
            PlayerSelect = Player.Length;
            Debug.LogError("플레이어 스킨 선택 초과!");
        }

        if(TileSelect > Tile.Length){
            TileSelect = Tile.Length;
            Debug.LogError("타일 스킨 선택 초과!");
        }
    }
}
