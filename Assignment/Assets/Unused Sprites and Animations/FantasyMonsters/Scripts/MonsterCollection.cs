using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.FantasyMonsters.Scripts
{
    /// <summary>
    /// Collect monster prefabs from specified path, scroll monsters in runtime
    /// </summary>
    public class MonsterCollection : MonoBehaviour
    {
        public string PrefabsPath;
        public List<GameObject> Monsters;
        public Transform Placement;
        public Text MonsterName;

        private int _monsterIndex;

        #if UNITY_EDITOR

        /// <summary>
        /// Called automatically when something was changed
        /// </summary>
        public void OnValidate()
        {
            Refresh();
        }

        /// <summary>
        /// Read all sprites from specified path again
        /// </summary>
        public void Refresh()
        {
            Monsters = Directory.GetFiles(PrefabsPath, "*.prefab", SearchOption.AllDirectories).Select(i => UnityEditor.AssetDatabase.LoadAssetAtPath(i, typeof(GameObject))).Cast<GameObject>().ToList();
        }

        #endif

        /// <summary>
        /// Called automatically on app start
        /// </summary>
        public void Start()
        {
            ChangeMonster(0);
        }

        /// <summary>
        /// Change monster (from list)
        /// </summary>
        public void ChangeMonster(int direction)
        {
            _monsterIndex += direction;

            if (_monsterIndex < 0)
            {
                _monsterIndex = Monsters.Count - 1;
            }

            if (_monsterIndex >= Monsters.Count)
            {
                _monsterIndex = 0;
            }

            Destroy(Placement.GetChild(0).gameObject);

            var monster = Instantiate(Monsters[_monsterIndex], Placement);

            monster.transform.localPosition = Vector3.zero;
            monster.transform.localScale = Vector3.one;
            MonsterName.text = GetMonsterName(Monsters[_monsterIndex]);
            StartCoroutine(PlayAnimation());
        }

        private static string GetMonsterName(GameObject prefab)
        {
            if (prefab == null) return "-";
            if (prefab.name.All(c => char.IsUpper(c))) return prefab.name;

            return Regex.Replace(Regex.Replace(prefab.name, "[A-Z]", " $0"), "([a-z])([1-9])", "$1 $2").Trim();
        }

        /// <summary>
        /// Play animation in next frame (after monster creation)
        /// </summary>
        private IEnumerator PlayAnimation()
        {
            yield return null;
            FindObjectOfType<MonsterAnimationManager>().PlayAnimation(0);
        }
    }
}