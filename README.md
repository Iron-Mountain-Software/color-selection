# Color Selection
Version: 1.0.2
A UI tool element for selecting a color while in gameplay.

## Package Mirrors:
[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODg3LnBuZw==/original/npRUfq.png'>](https://github.com/Iron-Mountain-Software/color-selection)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODk4LnBuZw==/original/Rv4m96.png'>](https://iron-mountain.itch.io/color-selection)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODkyLnBuZw==/original/Fq0ORM.png'>](https://www.npmjs.com/package/com.iron-mountain.color-selection)
## Key Scripts & Components:
1. public class **ColorSelector** : MonoBehaviour
   * Actions: 
      * public event Action ***OnSelectedColorChanged*** 
   * Properties: 
      * public Color ***SelectedColor***  { get; }
   * Methods: 
      * public virtual void ***OnPointerDown***(PointerEventData eventData)
      * public virtual void ***OnDrag***(PointerEventData eventData)
1. public class **SelectedColorGraphic** : MonoBehaviour
