using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Dropdown : MonoBehaviour, IPointerClickHandler
{
    public event System.Action<string> ValueChanged;
    public string Value { get; private set; }

    [Header("Values")]
    [SerializeField] private string[] values;

    [Header("Dropdown")]
    [SerializeField] private RectTransform content;
    [Space]
    [SerializeField] private TextMeshProUGUI dropdownLabel;

    [Header("Elements")]
    [SerializeField] private DropdownElement template;
    [Space]
    [SerializeField] private float spacing;

    private DropdownElement[] _elements;

    private bool isHided = true;

    private void Awake() {
        template.gameObject.SetActive(false);
        CreateElements();
    }

    public void SetValues(params string[] newValues){
        values = newValues;
        CreateElements();
    }

    private void CreateElements(){
        var elements = new DropdownElement[values.Length];

        for (int i = 0; i < values.Length; i++){
            if(_elements == null || i >= _elements.Length){
                elements[i] = Instantiate(template, content).GetComponent<DropdownElement>();
                elements[i].gameObject.SetActive(true);
            }
            else{
                elements[i] = _elements[i];
            }
        
            Vector2 targetPosition = new Vector2(0, (-template.Size.y - spacing) * (i + 1));
            elements[i].Init(values[i], targetPosition);
            elements[i].Hide();
            elements[i].OnClick += OnElementClick;
        }

        for (int i = elements.Length; i < _elements?.Length; i++){
            Destroy(_elements[i].gameObject);
        }

        _elements = elements;

        if(_elements.Length > 0)
            ChangeValue(_elements[0].Value);
    }

    public void OnPointerClick(PointerEventData eventData){
        isHided = !isHided;

        for (int i = 0; i < _elements.Length; i++){
            if(isHided)
                _elements[i].Hide();
            else
                _elements[i].Show();
        }
    }

    private void OnElementClick(string value){
        ChangeValue(value);
        ValueChanged?.Invoke(value);
    }

    private void ChangeValue(string value){
        Value = value;
        dropdownLabel.text = Value;
    }
}