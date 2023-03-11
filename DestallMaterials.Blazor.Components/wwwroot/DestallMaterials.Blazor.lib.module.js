window.scrollElement_X = function (elementId, x) {
    const elem = document.getElementById(elementId);
    elem.scrollTo(x, 0);
}

window.scrollElement_Y = function (elementId, y) {
    const elem = document.getElementById(elementId);
    elem.scrollTo(0, y);
}

window.Y_elementDeviation = function (elemId, containerId) {
    const elem = document.getElementById(elemId);
    const container = document.getElementById(containerId);

    const style = getComputedStyle(container);

    const rect = container.getBoundingClientRect();
    const elemRect = elem.getBoundingClientRect();

    const paddingTop = parseFloat(style.paddingTop) + parseFloat(style.webkitMarginBefore);
    const paddingBottom = parseFloat(style.paddingBottom) + parseFloat(style.webkitMarginAfter);

    const isElemBelowTop = (rect.top + paddingTop) - elemRect.top;
    const isElemAboveBottom = elemRect.bottom - (rect.bottom - paddingBottom);

    if (isElemBelowTop <= 0 && isElemAboveBottom <= 0) {
        return 0;
    }
    if (isElemBelowTop <= 0 && isElemAboveBottom > 0) {
        return -isElemAboveBottom;
    }
    if (isElemBelowTop > 0 && isElemAboveBottom <= 0) {
        return isElemBelowTop;
    }
    return isElemBelowTop - isElemAboveBottom;
}

window.X_elementDeviation = function (elemId, containerId) {
    const elem = document.getElementById(elemId);
    const container = document.getElementById(containerId);

    const style = getComputedStyle(container);

    const paddingLeft = parseFloat(style.paddingLeft);
    const paddingRight = parseFloat(style.paddingRight);

    const rect = container.getBoundingClientRect();
    const elemRect = elem.getBoundingClientRect();

    const isElemRighter = (rect.left + paddingLeft) - elemRect.left;
    const isElemLefter = elemRect.right - (rect.right - paddingRight);

    if (isElemRighter <= 0 && isElemLefter <= 0) {
        return 0;
    }
    if (isElemRighter <= 0 && isElemLefter > 0) {
        return -isElemLefter;
    }
    if (isElemRighter > 0 && isElemLefter <= 0) {
        return isElemRighter;
    }
    return isElemRighter - isElemLefter;
}

window.Y_scrollToFit = function (elemId, containerId) {
    const element = document.getElementById(elemId);

    element.scrollIntoView({ behavior: "smooth", block: "start" });
}

window.setCssVariableValue = function (elemId, variableName, value) {
    const elem = document.getElementById(elemId);
    elem.style.setProperty(variableName, value);
}

window.getItemScroll_Y = function (elementId) {
    const elem = document.getElementById(elementId);
    const result = elem.scrollTop;
    return result;
}

window.disableDefaultHandling = function (elemId, eventType) {
    const elem = document.getElementById(elemId);
    elem[eventType] = event => {
        event.preventDefault();
    }
    elem.removeEventListener(eventType, elem[eventType]);
}