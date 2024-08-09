using System.Collections.Generic;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    public GameConfig gameConfig;
    public SpinConfig spinConfig;
    public SpriteConfig spriteConfig;

    [SerializeField] private RowSlotController rowSlotController;

    private static GameInstaller _instance;
    public static GameInstaller Instance => _instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else
        {
            if (Application.isPlaying)
                Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartConfig();
    }

    private void StartConfig()
    {
        var rowsSlots = new List<List<SlotModel>>();

        foreach (var row in gameConfig.rowIDsOrder)
        {
            var slotsModels = new List<SlotModel>();

            foreach (var id in row.rowIDs)
            {
                var model = new SlotModel()
                {
                    slotID = id,
                    icon = spriteConfig.GetSprite(id)
                };

                slotsModels.Add(model);
            }

            rowsSlots.Add(slotsModels);
        }

        rowSlotController.InitializeRows(rowsSlots);
    }
}
