using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItemIcon : MonoBehaviour
{
    private Image _iconImage;

    private void Awake()
    {
        _iconImage = GetComponent<Image>();

        if (_iconImage.sprite == null)
            _iconImage.enabled = false;
    }

    public void SetItem(Sprite item)
    {
        if (item == null)
        {
            _iconImage.enabled = false;
        }
        else
        {
            _iconImage.enabled = true;
            _iconImage.sprite = item;
        }
    }

    public Sprite GetItem()
    {
        if (_iconImage.enabled == false)
            return null;

        return _iconImage.sprite;
    }
}