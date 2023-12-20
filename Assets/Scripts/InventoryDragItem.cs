using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class InventoryDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 _startPosition;
    private Transform _originalParent;
    private CanvasGroup _canvasGroup;
    private Canvas _parentCanvas;
    private IDragSource _source;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _parentCanvas = GetComponentInParent<Canvas>();
        _source = GetComponentInParent<IDragSource>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        _originalParent = transform.parent;

        _canvasGroup.blocksRaycasts = false;
        transform.SetParent(_parentCanvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _startPosition;
        _canvasGroup.blocksRaycasts = true;
        transform.SetParent(_originalParent);

        if (TryGetContainer(out IDragContainer container, eventData))
            DropItemIntoContainer(container);
    }

    private bool TryGetContainer(out IDragContainer container, PointerEventData eventData)
    {
        if (eventData.pointerEnter)
            container = eventData.pointerEnter.GetComponent<IDragContainer>();
        else
            container = null;

        return container != null;
    }

    private void DropItemIntoContainer(IDragContainer container)
    {
        if (ReferenceEquals(container, _source))
            return;

        if (container.GetItem() == null)
            Transfer(container);
    }

    private void Transfer(IDragContainer container)
    {
        Sprite draggingItem = _source.GetItem();

        if(draggingItem != null)
        {
            _source.RemoveItem();
            container.AddItem(draggingItem);
        }
    }
}