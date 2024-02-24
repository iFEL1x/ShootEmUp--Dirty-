using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField] private GameObject _character; 
        [SerializeField] private GameManager _gameManager;

        private void OnEnable()
        {
            this._character.GetComponent<HitPointsComponent>().OnHpEmpty += this.OnCharacterDeath;
        }

        private void OnDisable()
        {
            this._character.GetComponent<HitPointsComponent>().OnHpEmpty -= this.OnCharacterDeath;
        }
        
        private void OnCharacterDeath(GameObject _)
        {
            this._gameManager.FinishGame();
        }
    }
}
