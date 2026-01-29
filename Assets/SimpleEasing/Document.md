# Simple Easing

### Namespace

> Before using, make sure to include the namespace:

```csharp
using SimpleEasing;
```

---

## Overview

This package provides `EaseType` and `Easing` functionality for smooth animations.

* `EaseType` allows you to select the easing curve.
* `Easing` provides the calculation functions.

---

## Using Easing
Returns a value calculated based on the selected `EaseType`.
```csharp
  Ease.Easing(value, easeType);
```
The `value` must be normalized to work properly.

```csharp
using UnityEngine;
using SimpleEasing;

public class Test : MonoBehaviour
{
    private Vector2 startPosition = new Vector2(-10, 0);
    private Vector2 targetPosition = new Vector2(10, 0);

    private float duration = 1f;
    private float elapsed = 0f;

    private void Update()
    {
        // Time flow
        elapsed += Time.deltaTime;

        // Normalization
        float t = elapsed / duration;

        // Use Easing
        float easeT = Ease.Easing(t, EaseType.OutCirc);

        // Apply to position
        transform.position = Vector2.LerpUnclamped(startPosition, targetPosition, easeT);
    }
}
```


---

## EaseType

`EaseType` is an enum containing all available easing functions.

```csharp
using SimpleEasing;

public class Test : MonoBehaviour
{
    [SerializeField]
    private EaseType ease;
}
```

* You can modify `EaseType` directly in the Inspector.
* The graph in the Inspector updates to match the selected `EaseType`.

---

## Config

You can configure the graph at:

```
SimpleEasing/GraphConfig.asset
```

### Options

* **Enable Graph**

  > Enable or disable graph display.

* **Sample**

  > Number of samples used for the graph.

* **yMin**

  > Minimum Y value of the graph.

* **yMax**

  > Maximum Y value of the graph.
