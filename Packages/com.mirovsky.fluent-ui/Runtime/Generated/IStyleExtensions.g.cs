namespace FluentUI
{
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class FluentUIIStyleExtensions
    {
        public static TVisualElement AlignContent<TVisualElement>(this TVisualElement t, StyleEnum<Align> alignContent) where TVisualElement : IStyle
        {
            t.alignContent = alignContent;

            return t;
        }

        public static TVisualElement AlignItems<TVisualElement>(this TVisualElement t, StyleEnum<Align> alignItems) where TVisualElement : IStyle
        {
            t.alignItems = alignItems;

            return t;
        }

        public static TVisualElement AlignSelf<TVisualElement>(this TVisualElement t, StyleEnum<Align> alignSelf) where TVisualElement : IStyle
        {
            t.alignSelf = alignSelf;

            return t;
        }

        public static TVisualElement BackgroundColor<TVisualElement>(this TVisualElement t, StyleColor backgroundColor) where TVisualElement : IStyle
        {
            t.backgroundColor = backgroundColor;

            return t;
        }

        public static TVisualElement BackgroundImage<TVisualElement>(this TVisualElement t, StyleBackground backgroundImage) where TVisualElement : IStyle
        {
            t.backgroundImage = backgroundImage;

            return t;
        }

        public static TVisualElement BackgroundPositionX<TVisualElement>(this TVisualElement t, StyleBackgroundPosition backgroundPositionX) where TVisualElement : IStyle
        {
            t.backgroundPositionX = backgroundPositionX;

            return t;
        }

        public static TVisualElement BackgroundPositionY<TVisualElement>(this TVisualElement t, StyleBackgroundPosition backgroundPositionY) where TVisualElement : IStyle
        {
            t.backgroundPositionY = backgroundPositionY;

            return t;
        }

        public static TVisualElement BackgroundRepeat<TVisualElement>(this TVisualElement t, StyleBackgroundRepeat backgroundRepeat) where TVisualElement : IStyle
        {
            t.backgroundRepeat = backgroundRepeat;

            return t;
        }

        public static TVisualElement BackgroundSize<TVisualElement>(this TVisualElement t, StyleBackgroundSize backgroundSize) where TVisualElement : IStyle
        {
            t.backgroundSize = backgroundSize;

            return t;
        }

        public static TVisualElement BorderBottomColor<TVisualElement>(this TVisualElement t, StyleColor borderBottomColor) where TVisualElement : IStyle
        {
            t.borderBottomColor = borderBottomColor;

            return t;
        }

        public static TVisualElement BorderBottomLeftRadius<TVisualElement>(this TVisualElement t, StyleLength borderBottomLeftRadius) where TVisualElement : IStyle
        {
            t.borderBottomLeftRadius = borderBottomLeftRadius;

            return t;
        }

        public static TVisualElement BorderBottomRightRadius<TVisualElement>(this TVisualElement t, StyleLength borderBottomRightRadius) where TVisualElement : IStyle
        {
            t.borderBottomRightRadius = borderBottomRightRadius;

            return t;
        }

        public static TVisualElement BorderBottomWidth<TVisualElement>(this TVisualElement t, StyleFloat borderBottomWidth) where TVisualElement : IStyle
        {
            t.borderBottomWidth = borderBottomWidth;

            return t;
        }

        public static TVisualElement BorderLeftColor<TVisualElement>(this TVisualElement t, StyleColor borderLeftColor) where TVisualElement : IStyle
        {
            t.borderLeftColor = borderLeftColor;

            return t;
        }

        public static TVisualElement BorderLeftWidth<TVisualElement>(this TVisualElement t, StyleFloat borderLeftWidth) where TVisualElement : IStyle
        {
            t.borderLeftWidth = borderLeftWidth;

            return t;
        }

        public static TVisualElement BorderRightColor<TVisualElement>(this TVisualElement t, StyleColor borderRightColor) where TVisualElement : IStyle
        {
            t.borderRightColor = borderRightColor;

            return t;
        }

        public static TVisualElement BorderRightWidth<TVisualElement>(this TVisualElement t, StyleFloat borderRightWidth) where TVisualElement : IStyle
        {
            t.borderRightWidth = borderRightWidth;

            return t;
        }

        public static TVisualElement BorderTopColor<TVisualElement>(this TVisualElement t, StyleColor borderTopColor) where TVisualElement : IStyle
        {
            t.borderTopColor = borderTopColor;

            return t;
        }

        public static TVisualElement BorderTopLeftRadius<TVisualElement>(this TVisualElement t, StyleLength borderTopLeftRadius) where TVisualElement : IStyle
        {
            t.borderTopLeftRadius = borderTopLeftRadius;

            return t;
        }

        public static TVisualElement BorderTopRightRadius<TVisualElement>(this TVisualElement t, StyleLength borderTopRightRadius) where TVisualElement : IStyle
        {
            t.borderTopRightRadius = borderTopRightRadius;

            return t;
        }

        public static TVisualElement BorderTopWidth<TVisualElement>(this TVisualElement t, StyleFloat borderTopWidth) where TVisualElement : IStyle
        {
            t.borderTopWidth = borderTopWidth;

            return t;
        }

        public static TVisualElement Bottom<TVisualElement>(this TVisualElement t, StyleLength bottom) where TVisualElement : IStyle
        {
            t.bottom = bottom;

            return t;
        }

        public static TVisualElement Color<TVisualElement>(this TVisualElement t, StyleColor color) where TVisualElement : IStyle
        {
            t.color = color;

            return t;
        }

        public static TVisualElement Cursor<TVisualElement>(this TVisualElement t, StyleCursor cursor) where TVisualElement : IStyle
        {
            t.cursor = cursor;

            return t;
        }

        public static TVisualElement Display<TVisualElement>(this TVisualElement t, StyleEnum<DisplayStyle> display) where TVisualElement : IStyle
        {
            t.display = display;

            return t;
        }

        public static TVisualElement FlexBasis<TVisualElement>(this TVisualElement t, StyleLength flexBasis) where TVisualElement : IStyle
        {
            t.flexBasis = flexBasis;

            return t;
        }

        public static TVisualElement FlexDirection<TVisualElement>(this TVisualElement t, StyleEnum<FlexDirection> flexDirection) where TVisualElement : IStyle
        {
            t.flexDirection = flexDirection;

            return t;
        }

        public static TVisualElement FlexGrow<TVisualElement>(this TVisualElement t, StyleFloat flexGrow) where TVisualElement : IStyle
        {
            t.flexGrow = flexGrow;

            return t;
        }

        public static TVisualElement FlexShrink<TVisualElement>(this TVisualElement t, StyleFloat flexShrink) where TVisualElement : IStyle
        {
            t.flexShrink = flexShrink;

            return t;
        }

        public static TVisualElement FlexWrap<TVisualElement>(this TVisualElement t, StyleEnum<Wrap> flexWrap) where TVisualElement : IStyle
        {
            t.flexWrap = flexWrap;

            return t;
        }

        public static TVisualElement FontSize<TVisualElement>(this TVisualElement t, StyleLength fontSize) where TVisualElement : IStyle
        {
            t.fontSize = fontSize;

            return t;
        }

        public static TVisualElement Height<TVisualElement>(this TVisualElement t, StyleLength height) where TVisualElement : IStyle
        {
            t.height = height;

            return t;
        }

        public static TVisualElement JustifyContent<TVisualElement>(this TVisualElement t, StyleEnum<Justify> justifyContent) where TVisualElement : IStyle
        {
            t.justifyContent = justifyContent;

            return t;
        }

        public static TVisualElement Left<TVisualElement>(this TVisualElement t, StyleLength left) where TVisualElement : IStyle
        {
            t.left = left;

            return t;
        }

        public static TVisualElement LetterSpacing<TVisualElement>(this TVisualElement t, StyleLength letterSpacing) where TVisualElement : IStyle
        {
            t.letterSpacing = letterSpacing;

            return t;
        }

        public static TVisualElement MarginBottom<TVisualElement>(this TVisualElement t, StyleLength marginBottom) where TVisualElement : IStyle
        {
            t.marginBottom = marginBottom;

            return t;
        }

        public static TVisualElement MarginLeft<TVisualElement>(this TVisualElement t, StyleLength marginLeft) where TVisualElement : IStyle
        {
            t.marginLeft = marginLeft;

            return t;
        }

        public static TVisualElement MarginRight<TVisualElement>(this TVisualElement t, StyleLength marginRight) where TVisualElement : IStyle
        {
            t.marginRight = marginRight;

            return t;
        }

        public static TVisualElement MarginTop<TVisualElement>(this TVisualElement t, StyleLength marginTop) where TVisualElement : IStyle
        {
            t.marginTop = marginTop;

            return t;
        }

        public static TVisualElement MaxHeight<TVisualElement>(this TVisualElement t, StyleLength maxHeight) where TVisualElement : IStyle
        {
            t.maxHeight = maxHeight;

            return t;
        }

        public static TVisualElement MaxWidth<TVisualElement>(this TVisualElement t, StyleLength maxWidth) where TVisualElement : IStyle
        {
            t.maxWidth = maxWidth;

            return t;
        }

        public static TVisualElement MinHeight<TVisualElement>(this TVisualElement t, StyleLength minHeight) where TVisualElement : IStyle
        {
            t.minHeight = minHeight;

            return t;
        }

        public static TVisualElement MinWidth<TVisualElement>(this TVisualElement t, StyleLength minWidth) where TVisualElement : IStyle
        {
            t.minWidth = minWidth;

            return t;
        }

        public static TVisualElement Opacity<TVisualElement>(this TVisualElement t, StyleFloat opacity) where TVisualElement : IStyle
        {
            t.opacity = opacity;

            return t;
        }

        public static TVisualElement Overflow<TVisualElement>(this TVisualElement t, StyleEnum<Overflow> overflow) where TVisualElement : IStyle
        {
            t.overflow = overflow;

            return t;
        }

        public static TVisualElement PaddingBottom<TVisualElement>(this TVisualElement t, StyleLength paddingBottom) where TVisualElement : IStyle
        {
            t.paddingBottom = paddingBottom;

            return t;
        }

        public static TVisualElement PaddingLeft<TVisualElement>(this TVisualElement t, StyleLength paddingLeft) where TVisualElement : IStyle
        {
            t.paddingLeft = paddingLeft;

            return t;
        }

        public static TVisualElement PaddingRight<TVisualElement>(this TVisualElement t, StyleLength paddingRight) where TVisualElement : IStyle
        {
            t.paddingRight = paddingRight;

            return t;
        }

        public static TVisualElement PaddingTop<TVisualElement>(this TVisualElement t, StyleLength paddingTop) where TVisualElement : IStyle
        {
            t.paddingTop = paddingTop;

            return t;
        }

        public static TVisualElement Position<TVisualElement>(this TVisualElement t, StyleEnum<Position> position) where TVisualElement : IStyle
        {
            t.position = position;

            return t;
        }

        public static TVisualElement Right<TVisualElement>(this TVisualElement t, StyleLength right) where TVisualElement : IStyle
        {
            t.right = right;

            return t;
        }

        public static TVisualElement Rotate<TVisualElement>(this TVisualElement t, StyleRotate rotate) where TVisualElement : IStyle
        {
            t.rotate = rotate;

            return t;
        }

        public static TVisualElement Scale<TVisualElement>(this TVisualElement t, StyleScale scale) where TVisualElement : IStyle
        {
            t.scale = scale;

            return t;
        }

        public static TVisualElement TextOverflow<TVisualElement>(this TVisualElement t, StyleEnum<TextOverflow> textOverflow) where TVisualElement : IStyle
        {
            t.textOverflow = textOverflow;

            return t;
        }

        public static TVisualElement TextShadow<TVisualElement>(this TVisualElement t, StyleTextShadow textShadow) where TVisualElement : IStyle
        {
            t.textShadow = textShadow;

            return t;
        }

        public static TVisualElement Top<TVisualElement>(this TVisualElement t, StyleLength top) where TVisualElement : IStyle
        {
            t.top = top;

            return t;
        }

        public static TVisualElement TransformOrigin<TVisualElement>(this TVisualElement t, StyleTransformOrigin transformOrigin) where TVisualElement : IStyle
        {
            t.transformOrigin = transformOrigin;

            return t;
        }

        public static TVisualElement TransitionDelay<TVisualElement>(this TVisualElement t, StyleList<TimeValue> transitionDelay) where TVisualElement : IStyle
        {
            t.transitionDelay = transitionDelay;

            return t;
        }

        public static TVisualElement TransitionDuration<TVisualElement>(this TVisualElement t, StyleList<TimeValue> transitionDuration) where TVisualElement : IStyle
        {
            t.transitionDuration = transitionDuration;

            return t;
        }

        public static TVisualElement TransitionProperty<TVisualElement>(this TVisualElement t, StyleList<StylePropertyName> transitionProperty) where TVisualElement : IStyle
        {
            t.transitionProperty = transitionProperty;

            return t;
        }

        public static TVisualElement TransitionTimingFunction<TVisualElement>(this TVisualElement t, StyleList<EasingFunction> transitionTimingFunction) where TVisualElement : IStyle
        {
            t.transitionTimingFunction = transitionTimingFunction;

            return t;
        }

        public static TVisualElement Translate<TVisualElement>(this TVisualElement t, StyleTranslate translate) where TVisualElement : IStyle
        {
            t.translate = translate;

            return t;
        }

        public static TVisualElement UnityBackgroundImageTintColor<TVisualElement>(this TVisualElement t, StyleColor unityBackgroundImageTintColor) where TVisualElement : IStyle
        {
            t.unityBackgroundImageTintColor = unityBackgroundImageTintColor;

            return t;
        }

        public static TVisualElement UnityEditorTextRenderingMode<TVisualElement>(this TVisualElement t, StyleEnum<EditorTextRenderingMode> unityEditorTextRenderingMode) where TVisualElement : IStyle
        {
            t.unityEditorTextRenderingMode = unityEditorTextRenderingMode;

            return t;
        }

        public static TVisualElement UnityFont<TVisualElement>(this TVisualElement t, StyleFont unityFont) where TVisualElement : IStyle
        {
            t.unityFont = unityFont;

            return t;
        }

        public static TVisualElement UnityFontDefinition<TVisualElement>(this TVisualElement t, StyleFontDefinition unityFontDefinition) where TVisualElement : IStyle
        {
            t.unityFontDefinition = unityFontDefinition;

            return t;
        }

        public static TVisualElement UnityFontStyleAndWeight<TVisualElement>(this TVisualElement t, StyleEnum<FontStyle> unityFontStyleAndWeight) where TVisualElement : IStyle
        {
            t.unityFontStyleAndWeight = unityFontStyleAndWeight;

            return t;
        }

        public static TVisualElement UnityOverflowClipBox<TVisualElement>(this TVisualElement t, StyleEnum<OverflowClipBox> unityOverflowClipBox) where TVisualElement : IStyle
        {
            t.unityOverflowClipBox = unityOverflowClipBox;

            return t;
        }

        public static TVisualElement UnityParagraphSpacing<TVisualElement>(this TVisualElement t, StyleLength unityParagraphSpacing) where TVisualElement : IStyle
        {
            t.unityParagraphSpacing = unityParagraphSpacing;

            return t;
        }

        public static TVisualElement UnitySliceBottom<TVisualElement>(this TVisualElement t, StyleInt unitySliceBottom) where TVisualElement : IStyle
        {
            t.unitySliceBottom = unitySliceBottom;

            return t;
        }

        public static TVisualElement UnitySliceLeft<TVisualElement>(this TVisualElement t, StyleInt unitySliceLeft) where TVisualElement : IStyle
        {
            t.unitySliceLeft = unitySliceLeft;

            return t;
        }

        public static TVisualElement UnitySliceRight<TVisualElement>(this TVisualElement t, StyleInt unitySliceRight) where TVisualElement : IStyle
        {
            t.unitySliceRight = unitySliceRight;

            return t;
        }

        public static TVisualElement UnitySliceScale<TVisualElement>(this TVisualElement t, StyleFloat unitySliceScale) where TVisualElement : IStyle
        {
            t.unitySliceScale = unitySliceScale;

            return t;
        }

        public static TVisualElement UnitySliceTop<TVisualElement>(this TVisualElement t, StyleInt unitySliceTop) where TVisualElement : IStyle
        {
            t.unitySliceTop = unitySliceTop;

            return t;
        }

        public static TVisualElement UnitySliceType<TVisualElement>(this TVisualElement t, StyleEnum<SliceType> unitySliceType) where TVisualElement : IStyle
        {
            t.unitySliceType = unitySliceType;

            return t;
        }

        public static TVisualElement UnityTextAlign<TVisualElement>(this TVisualElement t, StyleEnum<TextAnchor> unityTextAlign) where TVisualElement : IStyle
        {
            t.unityTextAlign = unityTextAlign;

            return t;
        }

        public static TVisualElement UnityTextGenerator<TVisualElement>(this TVisualElement t, StyleEnum<TextGeneratorType> unityTextGenerator) where TVisualElement : IStyle
        {
            t.unityTextGenerator = unityTextGenerator;

            return t;
        }

        public static TVisualElement UnityTextOutlineColor<TVisualElement>(this TVisualElement t, StyleColor unityTextOutlineColor) where TVisualElement : IStyle
        {
            t.unityTextOutlineColor = unityTextOutlineColor;

            return t;
        }

        public static TVisualElement UnityTextOutlineWidth<TVisualElement>(this TVisualElement t, StyleFloat unityTextOutlineWidth) where TVisualElement : IStyle
        {
            t.unityTextOutlineWidth = unityTextOutlineWidth;

            return t;
        }

        public static TVisualElement UnityTextOverflowPosition<TVisualElement>(this TVisualElement t, StyleEnum<TextOverflowPosition> unityTextOverflowPosition) where TVisualElement : IStyle
        {
            t.unityTextOverflowPosition = unityTextOverflowPosition;

            return t;
        }

        public static TVisualElement Visibility<TVisualElement>(this TVisualElement t, StyleEnum<Visibility> visibility) where TVisualElement : IStyle
        {
            t.visibility = visibility;

            return t;
        }

        public static TVisualElement WhiteSpace<TVisualElement>(this TVisualElement t, StyleEnum<WhiteSpace> whiteSpace) where TVisualElement : IStyle
        {
            t.whiteSpace = whiteSpace;

            return t;
        }

        public static TVisualElement Width<TVisualElement>(this TVisualElement t, StyleLength width) where TVisualElement : IStyle
        {
            t.width = width;

            return t;
        }

        public static TVisualElement WordSpacing<TVisualElement>(this TVisualElement t, StyleLength wordSpacing) where TVisualElement : IStyle
        {
            t.wordSpacing = wordSpacing;

            return t;
        }
    }
}
