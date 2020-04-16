﻿using System;
using System.Linq;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Collections.Generic;
using FamiStudio.Properties;
using Color = System.Drawing.Color;
using System.Diagnostics;

#if FAMISTUDIO_WINDOWS
    using RenderBrush    = SharpDX.Direct2D1.Brush;
    using RenderBitmap   = SharpDX.Direct2D1.Bitmap;
    using RenderFont     = SharpDX.DirectWrite.TextFormat;
    using RenderControl  = FamiStudio.Direct2DControl;
    using RenderGraphics = FamiStudio.Direct2DGraphics;
    using RenderTheme    = FamiStudio.Direct2DTheme;
#else
    using RenderBrush    = FamiStudio.GLBrush;
    using RenderBitmap   = FamiStudio.GLBitmap;
    using RenderFont     = FamiStudio.GLFont;
    using RenderControl  = FamiStudio.GLControl;
    using RenderGraphics = FamiStudio.GLGraphics;
    using RenderTheme    = FamiStudio.GLTheme;
#endif

namespace FamiStudio
{
    public class ProjectExplorer : RenderControl
    {
        const int DefaultExpandButtonSizeX    = 8;
        const int DefaultButtonIconPosX       = 4;
        const int DefaultExpandButtonPosX     = 3;
        const int DefaultExpandButtonPosY     = 8;
        const int DefaultButtonIconPosY       = 4;
        const int DefaultButtonTextPosX       = 24;
        const int DefaultButtonTextPosY       = 5;
        const int DefaultButtonTextNoIconPosX = 5;
        const int DefaultSubButtonSpacingX    = 20;
        const int DefaultSubButtonPosY        = 4;
        const int DefaultScrollBarSizeX       = 8;
        const int DefaultButtonSizeY          = 23;
        const int DefaultSliderPosX           = 100;
        const int DefaultSliderPosY           = 4;
        const int DefaultSliderSizeX          = 96;
        const int DefaultSliderSizeY          = 16;
        const int DefaultSliderThumbSizeX     = 4;
        const int DefaultSliderTextPosX       = 110;
        const int DefaultCheckBoxPosX         = 20;
        const int DefaultCheckBoxPosY         = 4;

        int expandButtonSizeX;
        int buttonIconPosX;
        int buttonIconPosY;
        int buttonTextPosX;
        int buttonTextPosY;
        int buttonTextNoIconPosX;
        int expandButtonPosX;
        int expandButtonPosY;
        int subButtonSpacingX;
        int subButtonPosY;
        int buttonSizeY;
        int sliderPosX;
        int sliderPosY;
        int sliderSizeX;
        int sliderSizeY;
        int sliderThumbSizeX;
        int sliderTextPosX;
        int checkBoxPosX;
        int checkBoxPosY;
        int virtualSizeY;
        int scrollBarSizeX;
        bool needsScrollBar;

        enum ButtonType
        {
            ProjectSettings,
            SongHeader,
            Song,
            InstrumentHeader,
            Instrument,
            ParamCheckbox,
            ParamSlider,
            ParamList,
            Max
        };

        enum SubButtonType
        {
            // Let's keep this enum and Envelope.XXX values in sync for convenience.
            VolumeEnvelope = Envelope.Volume,
            ArpeggioEnvelope = Envelope.Arpeggio,
            PitchEnvelope = Envelope.Pitch,
            DutyCycle = Envelope.DutyCycle,
            FdsWaveformEnvelope = Envelope.FdsWaveform,
            FdsModulationEnvelope = Envelope.FdsModulation,
            N163WaveformEnvelope = Envelope.N163Waveform,
            EnvelopeMax = Envelope.Count,

            // Other buttons
            Add,
            DPCM,
            LoadInstrument,
            ExpandInstrument,
            Max
        }

        // From right to left. Looks more visually pleasing than the enum order.
        static readonly int[] EnvelopeDisplayOrder =
        {
            Envelope.Arpeggio,
            Envelope.Pitch,
            Envelope.Volume,
            Envelope.DutyCycle,
            Envelope.FdsModulation,
            Envelope.FdsWaveform,
            Envelope.N163Waveform
        };

        class Button
        {
            public ButtonType type;
            public int instrumentParam = -1;
            public Song song;
            public Instrument instrument;
            public ProjectExplorer projectExplorer;

            public Button(ProjectExplorer pe)
            {
                projectExplorer = pe;
            }

            public SubButtonType[] GetSubButtons(out bool[] active)
            {
                switch (type)
                {
                    case ButtonType.SongHeader:
                        active = new[] { true };
                        return new[] { SubButtonType.Add };
                    case ButtonType.InstrumentHeader:
                        active = new[] { true ,true };
                        return new[] { SubButtonType.Add ,
                                       SubButtonType.LoadInstrument };
                    case ButtonType.Instrument:
                        if (instrument == null)
                        {
                            active = new[] { true };
                            return new[] { SubButtonType.DPCM };
                        }
                        else
                        {
                            var expandButton = projectExplorer.ShowExpandButtons() && instrument.IsExpansionInstrument;
                            var numSubButtons = instrument.NumActiveEnvelopes + (expandButton ? 1 : 0);
                            var buttons = new SubButtonType[numSubButtons];
                            active = new bool[numSubButtons];

                            for (int i = 0, j = 0; i < Envelope.Count; i++)
                            {
                                int idx = EnvelopeDisplayOrder[i];
                                if (instrument.Envelopes[idx] != null)
                                {
                                    buttons[j] = (SubButtonType)idx;
                                    active[j] = !instrument.Envelopes[idx].IsEmpty; 
                                    j++;
                                }
                            }

                            if (expandButton)
                            {
                                buttons[numSubButtons - 1] = SubButtonType.ExpandInstrument;
                                active[numSubButtons - 1]  = true;
                            }

                            return buttons;
                        }
                }

                active = null;
                return null;
            }

            public string GetText(Project project)
            {
                switch (type)
                {
                    case ButtonType.ProjectSettings: return $"{project.Name} ({project.Author})";
                    case ButtonType.SongHeader: return "Songs";
                    case ButtonType.Song: return song.Name;
                    case ButtonType.InstrumentHeader: return "Instruments";
                    case ButtonType.Instrument: return instrument == null ? "DPCM Samples" : instrument.Name;
                    case ButtonType.ParamCheckbox:
                    case ButtonType.ParamSlider:
                    case ButtonType.ParamList: return Instrument.GetRealTimeParamName(instrumentParam);
                }

                return "";
            }

            public Color GetColor()
            {
                switch (type)
                {
                    case ButtonType.SongHeader:
                    case ButtonType.InstrumentHeader: return ThemeBase.LightGreyFillColor2;
                    case ButtonType.Song: return song.Color;
                    case ButtonType.ParamCheckbox:
                    case ButtonType.ParamSlider:
                    case ButtonType.ParamList:
                    case ButtonType.Instrument: return instrument == null ? ThemeBase.LightGreyFillColor1 : instrument.Color;
                }

                return ThemeBase.LightGreyFillColor1;
            }

            public RenderFont GetFont(Song selectedSong, Instrument selectedInstrument)
            {
                if (type == ButtonType.ProjectSettings)
                {
                    return ThemeBase.FontMediumBoldCenterEllipsis;
                }
                if (type == ButtonType.SongHeader || type == ButtonType.InstrumentHeader)
                {
                    return ThemeBase.FontMediumBoldCenter;
                }
                else if ((type == ButtonType.Song && song == selectedSong) ||
                         (type == ButtonType.Instrument && instrument == selectedInstrument))
                {
                    return ThemeBase.FontMediumBold;
                }
                else
                {
                    return ThemeBase.FontMedium;
                }
            }

            public RenderBitmap GetIcon()
            {
                if (type == ButtonType.Song)
                {
                    return projectExplorer.bmpSong;
                }
                else if (type == ButtonType.Instrument)
                {
                    var expType = instrument != null ? instrument.ExpansionType : Project.ExpansionNone;
                    return projectExplorer.bmpInstrument[expType];
                }
                return null;
            }

            public RenderBitmap GetIcon(SubButtonType sub)
            {
                switch (sub)
                {
                    case SubButtonType.Add:
                        return projectExplorer.bmpAdd;
                    case SubButtonType.DPCM:
                        return projectExplorer.bmpDPCM;
                    case SubButtonType.LoadInstrument:
                        return projectExplorer.bmpLoadInstrument;
                    case SubButtonType.ExpandInstrument:
                        return projectExplorer.expandedInstrument == instrument ? projectExplorer.bmpExpanded : projectExplorer.bmpExpand;
                }

                return projectExplorer.bmpEnvelopes[(int)sub];
            }
        }

        enum CaptureOperation
        {
            None,
            DragInstrument,
            MoveSlider
        };

        static readonly bool[] captureNeedsThreshold = new[]
        {
            false,
            true,
            false
        };

        int scrollY = 0;
        int mouseLastX = 0;
        int mouseLastY = 0;
        int captureMouseX = -1;
        int captureMouseY = -1;
        int envelopeDragIdx = -1;
        bool captureThresholdMet = false;
        Button sliderDragButton = null;
        CaptureOperation captureOperation = CaptureOperation.None;
        Song selectedSong = null;
        Instrument draggedInstrument = null;
        Instrument selectedInstrument = null; // null = DPCM
        Instrument expandedInstrument = null;
        List<Button> buttons = new List<Button>();

        RenderTheme theme;

        RenderBrush    sliderFillBrush;
        RenderBitmap   bmpSong;
        RenderBitmap   bmpAdd;
        RenderBitmap   bmpDPCM;
        RenderBitmap   bmpLoadInstrument;
        RenderBitmap   bmpExpand;
        RenderBitmap   bmpExpanded;
        RenderBitmap   bmpCheckBoxYes;
        RenderBitmap   bmpCheckBoxNo;
        RenderBitmap   bmpButtonLeft;
        RenderBitmap   bmpButtonRight;
        RenderBitmap[] bmpInstrument = new RenderBitmap[Project.ExpansionCount];
        RenderBitmap[] bmpEnvelopes = new RenderBitmap[Envelope.Count];

        public Song SelectedSong => selectedSong;
        public Instrument SelectedInstrument => selectedInstrument;

        public delegate void EmptyDelegate();
        public delegate void InstrumentEnvelopeDelegate(Instrument instrument, int envelope);
        public delegate void InstrumentDelegate(Instrument instrument);
        public delegate void InstrumentPointDelegate(Instrument instrument, Point pos);
        public delegate void SongDelegate(Song song);

        public event InstrumentEnvelopeDelegate InstrumentEdited;
        public event InstrumentDelegate InstrumentSelected;
        public event InstrumentDelegate InstrumentColorChanged;
        public event InstrumentDelegate InstrumentReplaced;
        public event InstrumentDelegate InstrumentDeleted;
        public event InstrumentPointDelegate InstrumentDraggedOutside;
        public event SongDelegate SongModified;
        public event SongDelegate SongSelected;
        public event EmptyDelegate ProjectModified;

        public ProjectExplorer()
        {
            UpdateRenderCoords();
        }

        private void UpdateRenderCoords()
        {
            var scaling = RenderTheme.MainWindowScaling;

            expandButtonSizeX    = (int)(DefaultExpandButtonSizeX * scaling);
            buttonIconPosX       = (int)(DefaultButtonIconPosX * scaling);      
            buttonIconPosY       = (int)(DefaultButtonIconPosY * scaling);      
            buttonTextPosX       = (int)(DefaultButtonTextPosX * scaling);      
            buttonTextPosY       = (int)(DefaultButtonTextPosY * scaling);
            buttonTextNoIconPosX = (int)(DefaultButtonTextNoIconPosX * scaling);
            expandButtonPosX     = (int)(DefaultExpandButtonPosX * scaling);
            expandButtonPosY     = (int)(DefaultExpandButtonPosY * scaling);
            subButtonSpacingX    = (int)(DefaultSubButtonSpacingX * scaling);   
            subButtonPosY        = (int)(DefaultSubButtonPosY * scaling);       
            buttonSizeY          = (int)(DefaultButtonSizeY * scaling);
            sliderPosX           = (int)(DefaultSliderPosX * scaling);
            sliderPosY           = (int)(DefaultSliderPosY * scaling);
            sliderSizeX          = (int)(DefaultSliderSizeX * scaling);
            sliderSizeY          = (int)(DefaultSliderSizeY * scaling);
            sliderThumbSizeX     = (int)(DefaultSliderThumbSizeX * scaling);
            sliderTextPosX       = (int)(DefaultSliderTextPosX * scaling);
            checkBoxPosX         = (int)(DefaultCheckBoxPosX * scaling);
            checkBoxPosY         = (int)(DefaultCheckBoxPosY * scaling);
            virtualSizeY         = App?.Project == null ? Height : buttons.Count * buttonSizeY;
            needsScrollBar       = virtualSizeY > Height; 
            scrollBarSizeX       = needsScrollBar ? (int)(DefaultScrollBarSizeX * scaling) : 0;      
        }

        public void Reset()
        {
            selectedSong = App.Project.Songs[0];
            selectedInstrument = App.Project.Instruments.Count > 0 ? App.Project.Instruments[0] : null;
            expandedInstrument = null;
            SongSelected?.Invoke(selectedSong);
            RefreshButtons();
            ConditionalInvalidate();
        }

        public void RefreshButtons()
        {
            Debug.Assert(captureOperation != CaptureOperation.MoveSlider);

            buttons.Clear();
            buttons.Add(new Button(this) { type = ButtonType.ProjectSettings });
            buttons.Add(new Button(this) { type = ButtonType.SongHeader });

            foreach (var song in App.Project.Songs)
                buttons.Add(new Button(this) { type = ButtonType.Song, song = song });

            buttons.Add(new Button(this) { type = ButtonType.InstrumentHeader });
            buttons.Add(new Button(this) { type = ButtonType.Instrument }); // null instrument = DPCM

            foreach (var instrument in App.Project.Instruments)
            {
                buttons.Add(new Button(this) { type = ButtonType.Instrument, instrument = instrument });

                if (instrument != null && instrument == expandedInstrument)
                {
                    var instrumentParams = instrument.GetRealTimeParams();

                    if (instrumentParams != null)
                    {
                        foreach (var param in instrumentParams)
                        {
                            var widgetType = ButtonType.ParamSlider;

                            if (Instrument.IsRealTimeParamList(param))
                                widgetType = ButtonType.ParamList;
                            else if (Instrument.GetRealTimeParamMaxValue(param) == 1)
                                widgetType = ButtonType.ParamCheckbox;

                            buttons.Add(new Button(this) { type = widgetType, instrumentParam = param, instrument = instrument });
                        }
                    }
                }
            }

            UpdateRenderCoords();
        }

        protected override void OnRenderInitialized(RenderGraphics g)
        {
            theme = RenderTheme.CreateResourcesForGraphics(g);

            bmpInstrument[Project.ExpansionNone]    = g.CreateBitmapFromResource("Instrument");
            bmpInstrument[Project.ExpansionVrc6]    = g.CreateBitmapFromResource("InstrumentKonami");
            bmpInstrument[Project.ExpansionVrc7]    = g.CreateBitmapFromResource("InstrumentKonami");
            bmpInstrument[Project.ExpansionFds]     = g.CreateBitmapFromResource("InstrumentFds");
            bmpInstrument[Project.ExpansionMmc5]    = g.CreateBitmapFromResource("Instrument");
            bmpInstrument[Project.ExpansionN163]    = g.CreateBitmapFromResource("InstrumentNamco");
            bmpInstrument[Project.ExpansionS5B]     = g.CreateBitmapFromResource("InstrumentSunsoft");

            bmpEnvelopes[Envelope.Volume]        = g.CreateBitmapFromResource("Volume");
            bmpEnvelopes[Envelope.Arpeggio]      = g.CreateBitmapFromResource("Arpeggio");
            bmpEnvelopes[Envelope.Pitch]         = g.CreateBitmapFromResource("Pitch");
            bmpEnvelopes[Envelope.DutyCycle]     = g.CreateBitmapFromResource("Duty");
            bmpEnvelopes[Envelope.FdsWaveform]   = g.CreateBitmapFromResource("Wave");
            bmpEnvelopes[Envelope.FdsModulation] = g.CreateBitmapFromResource("Mod");
            bmpEnvelopes[Envelope.N163Waveform]  = g.CreateBitmapFromResource("Wave");

            bmpExpand = g.CreateBitmapFromResource("InstrumentExpand");
            bmpExpanded = g.CreateBitmapFromResource("InstrumentExpanded");
            bmpCheckBoxYes = g.CreateBitmapFromResource("CheckBoxYes");
            bmpCheckBoxNo = g.CreateBitmapFromResource("CheckBoxNo");
            bmpButtonLeft = g.CreateBitmapFromResource("ButtonLeft");
            bmpButtonRight = g.CreateBitmapFromResource("ButtonRight");
            bmpSong = g.CreateBitmapFromResource("Music");
            bmpAdd = g.CreateBitmapFromResource("Add");
            bmpDPCM = g.CreateBitmapFromResource("DPCM");
            bmpLoadInstrument = g.CreateBitmapFromResource("InstrumentOpen");
            sliderFillBrush = g.CreateSolidBrush(Color.FromArgb(64, Color.Black));
        }

        public void ConditionalInvalidate()
        {
            Invalidate();
        }

        protected bool ShowExpandButtons()
        {
            if (App.Project.ExpansionAudio != Project.ExpansionNone)
                return App.Project.Instruments.Find(i => i.GetRealTimeParams() != null) != null;

            return false;
        }

        protected override void OnRender(RenderGraphics g)
        {
            g.Clear(ThemeBase.DarkGreyFillColor1);
            g.DrawLine(0, 0, 0, Height, theme.BlackBrush);

            var showExpandButton = ShowExpandButtons();
            var actualWidth = Width - scrollBarSizeX;
            var firstParam = true;
            var y = -scrollY;

            for (int i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i];
                var icon = button.GetIcon();

                g.PushTranslation(0, y);

                if (button.type == ButtonType.ParamCheckbox || 
                    button.type == ButtonType.ParamSlider   || 
                    button.type == ButtonType.ParamList)
                {
                    if (firstParam)
                    {
                        var numParamButtons = 1;

                        for (int j = i + 1; j < buttons.Count; j++, numParamButtons++)
                        {
                            if (buttons[j].type != ButtonType.ParamCheckbox &&
                                buttons[j].type != ButtonType.ParamSlider &&
                                buttons[j].type != ButtonType.ParamList)
                            { 
                                break;
                            }
                        }

                        g.FillAndDrawRectangle(0, 0, actualWidth, numParamButtons * buttonSizeY, g.GetVerticalGradientBrush(button.GetColor(), numParamButtons * buttonSizeY, 0.8f), theme.BlackBrush);
                        firstParam = false;
                    }
                }
                else
                {
                    g.FillAndDrawRectangle(0, 0, actualWidth, buttonSizeY, g.GetVerticalGradientBrush(button.GetColor(), buttonSizeY, 0.8f), theme.BlackBrush);
                }

                var leftPadding = 0;
                var leftAligned = button.type == ButtonType.Instrument || button.type == ButtonType.Song || button.type == ButtonType.ParamSlider || button.type == ButtonType.ParamCheckbox || button.type == ButtonType.ParamList;

                if (showExpandButton && leftAligned)
                {
                    g.PushTranslation(1 + expandButtonSizeX, 0);
                    leftPadding = expandButtonSizeX;
                }

                g.DrawText(button.GetText(App.Project), button.GetFont(selectedSong, selectedInstrument), icon == null ? buttonTextNoIconPosX : buttonTextPosX, buttonTextPosY, theme.BlackBrush, actualWidth - buttonTextNoIconPosX * 2);

                if (icon != null)
                    g.DrawBitmap(icon, buttonIconPosX, buttonIconPosY);

                if (leftPadding != 0)
                    g.PopTransform();

                if (button.instrumentParam >= 0)
                {
                    var paramMin = Instrument.GetRealTimeParamMinValue(button.instrumentParam);
                    var paramMax = Instrument.GetRealTimeParamMaxValue(button.instrumentParam);
                    var paramVal = button.instrument.GetRealTimeParamValue(button.instrumentParam);
                    var paramStr = button.instrument.GetRealTimeParamString(button.instrumentParam);

                    if (button.type == ButtonType.ParamSlider)
                    {
                        var valSizeX = (int)Math.Round((paramVal - paramMin) / (float)(paramMax - paramMin) * sliderSizeX);

                        g.PushTranslation(actualWidth - sliderPosX, sliderPosY);
                        g.FillRectangle(0, 0, valSizeX, sliderSizeY, sliderFillBrush);
                        g.DrawRectangle(0, 0, sliderSizeX, sliderSizeY, theme.BlackBrush);
                        g.DrawText(paramStr, ThemeBase.FontMediumCenter, 0, buttonTextPosY - sliderPosY, theme.BlackBrush, sliderSizeX);
                        g.PopTransform();
                    }
                    else if (button.type == ButtonType.ParamCheckbox)
                    {
                        g.DrawBitmap(paramVal == 0 ? bmpCheckBoxNo : bmpCheckBoxYes, actualWidth - checkBoxPosX, checkBoxPosY);
                    }
                    else if (button.type == ButtonType.ParamList)
                    {
                        var paramPrev = button.instrument.GetRealTimeParamPrevValue(button.instrumentParam, paramVal);
                        var paramNext = button.instrument.GetRealTimeParamNextValue(button.instrumentParam, paramVal);

                        g.PushTranslation(actualWidth - sliderPosX, sliderPosY);
                        g.DrawBitmap(bmpButtonLeft, 0, 0, paramVal == paramPrev ? 0.25f : 1.0f);
                        g.DrawBitmap(bmpButtonRight, sliderSizeX - bmpButtonRight.Size.Width, 0, paramVal == paramNext ? 0.25f : 1.0f);
                        g.DrawText(paramStr, ThemeBase.FontMediumCenter, 0, buttonTextPosY - sliderPosY, theme.BlackBrush, sliderSizeX);
                        g.PopTransform();
                    }
                }
                else
                {
                    var subButtons = button.GetSubButtons(out var active);
                    if (subButtons != null)
                    {
                        for (int j = 0, x = actualWidth - subButtonSpacingX; j < subButtons.Length; j++, x -= subButtonSpacingX)
                        {
                            if (subButtons[j] == SubButtonType.ExpandInstrument)
                                g.DrawBitmap(button.GetIcon(subButtons[j]), expandButtonPosX, expandButtonPosY);
                            else
                                g.DrawBitmap(button.GetIcon(subButtons[j]), x, subButtonPosY, active[j] ? 1.0f : 0.2f);
                        }
                    }
                }

                g.PopTransform();
                y += buttonSizeY;
            }

            if (needsScrollBar)
            {
                int virtualSizeY   = this.virtualSizeY;
                int scrollBarSizeY = (int)Math.Round(Height * (Height  / (float)virtualSizeY));
                int scrollBarPosY  = (int)Math.Round(Height * (scrollY / (float)virtualSizeY));

                g.FillAndDrawRectangle(actualWidth, 0, Width - 1, Height, theme.DarkGreyFillBrush1, theme.BlackBrush);
                g.FillAndDrawRectangle(actualWidth, scrollBarPosY, Width - 1, scrollBarPosY + scrollBarSizeY, theme.LightGreyFillBrush1, theme.BlackBrush);
            }
        }

        private void ClampScroll()
        {
            int minScrollY = 0;
            int maxScrollY = Math.Max(virtualSizeY - Height, 0);

            if (scrollY < minScrollY) scrollY = minScrollY;
            if (scrollY > maxScrollY) scrollY = maxScrollY;
        }

        private void DoScroll(int deltaY)
        {
            scrollY -= deltaY;
            ClampScroll();
            ConditionalInvalidate();
        }

        protected void UpdateCursor()
        {
            if (captureOperation == CaptureOperation.DragInstrument && captureThresholdMet)
            {
#if !FAMISTUDIO_LINUX
                // TODO LINUX: Cursors
                Cursor.Current = envelopeDragIdx == -1 ? Cursors.DragCursor : Cursors.CopyCursor;
#endif
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private int GetButtonAtCoord(int x, int y, out SubButtonType sub)
        {
            var buttonIndex = (y + scrollY) / buttonSizeY;
            sub = SubButtonType.Max;

            if (buttonIndex >= 0 && buttonIndex < buttons.Count)
            {
                var button = buttons[buttonIndex];

                if (ShowExpandButtons() && button.instrument != null && button.instrument.IsExpansionInstrument && x < (expandButtonPosX + expandButtonSizeX))
                {
                    sub = SubButtonType.ExpandInstrument;
                    return buttonIndex;
                }

                var subButtons = button.GetSubButtons(out _);
                if (subButtons != null)
                {
                    y -= (buttonIndex * buttonSizeY - scrollY);

                    for (int i = 0; i < subButtons.Length; i++)
                    {
                        int sx = Width - scrollBarSizeX - subButtonSpacingX * (i + 1);
                        int sy = subButtonPosY;
                        int dx = x - sx;
                        int dy = y - sy;

                        if (dx >= 0 && dx < 16 * RenderTheme.MainWindowScaling &&
                            dy >= 0 && dy < 16 * RenderTheme.MainWindowScaling)
                        {
                            sub = subButtons[i];
                            break;
                        }
                        
                    }
                }

                return buttonIndex;
            }
            else
            {
                return -1;
            }
        }

        private void UpdateToolTip(MouseEventArgs e)
        {
            var tooltip = "";
            var buttonIdx = GetButtonAtCoord(e.X, e.Y, out var subButtonType);

            if (buttonIdx >= 0)
            {
                var buttonType = buttons[buttonIdx].type;

                if (buttonType == ButtonType.SongHeader && subButtonType == SubButtonType.Add)
                {
                    tooltip = "{MouseLeft} Add new song";
                }
                else if (buttonType == ButtonType.Song)
                {
                    tooltip = "{MouseLeft} Make song current - {MouseLeft}{MouseLeft} Song properties - {MouseRight} Delete song";
                }
                else if (buttonType == ButtonType.InstrumentHeader && subButtonType == SubButtonType.Add)
                {
                    tooltip = "{MouseLeft} Add new instrument";
                }
                else if (buttonType == ButtonType.ProjectSettings)
                {
                    tooltip = "{MouseLeft}{MouseLeft} Project properties";
                }
                else if (buttonType == ButtonType.ParamCheckbox && e.X >= Width - scrollBarSizeX - checkBoxPosX)
                {
                    tooltip = "{MouseLeft} Toggle value";
                }
                else if (buttonType == ButtonType.ParamSlider && e.X >= Width - scrollBarSizeX - sliderPosX)
                {
                    tooltip = "{MouseLeft} {Drag} Change value - {Shift} {MouseLeft} {Drag} Change value (fine)";
                    
                }
                else if (buttonType == ButtonType.ParamList && e.X >= Width - scrollBarSizeX - sliderPosX)
                {
                    tooltip = "{MouseLeft} Change value";
                }
                else if (buttonType == ButtonType.Instrument)
                {
                    if (subButtonType == SubButtonType.Max)
                    {
                        if (buttons[buttonIdx].instrument == null)
                            tooltip = "{MouseLeft} Select instrument";
                        else
                            tooltip = "{MouseLeft} Select instrument - {MouseLeft}{MouseLeft} Instrument properties\n{MouseRight} Delete instrument - {Drag} Replace instrument";
                    }
                    else
                    {
                        if (subButtonType == SubButtonType.DPCM)
                            tooltip = "{MouseLeft} Edit DPCM samples";
                        else if (subButtonType < SubButtonType.EnvelopeMax)
                            tooltip = $"{{MouseLeft}} Edit {Envelope.EnvelopeNames[(int)subButtonType].ToLower()} envelope - {{MouseRight}} Delete envelope - {{Drag}} Copy envelope";
                    }
                }
            }

            App.ToolTip = tooltip;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            bool left   = e.Button.HasFlag(MouseButtons.Left);
            bool middle = e.Button.HasFlag(MouseButtons.Middle) || (e.Button.HasFlag(MouseButtons.Left) && ModifierKeys.HasFlag(Keys.Alt));

            UpdateToolTip(e);

            if (captureOperation != CaptureOperation.None && !captureThresholdMet)
            {
                if (Math.Abs(e.X - captureMouseX) > 4 ||
                    Math.Abs(e.Y - captureMouseY) > 4)
                {
                    captureThresholdMet = true;
                }
            }

            if (captureOperation != CaptureOperation.None && captureThresholdMet)
            {
                if (captureOperation == CaptureOperation.MoveSlider)
                {
                    UpdateSliderValue(sliderDragButton, e);
                    ConditionalInvalidate();
                }
            }

            if (middle)
            {
                int deltaY = e.Y - mouseLastY;
                DoScroll(deltaY);
            }

            mouseLastX = e.X;
            mouseLastY = e.Y;

            UpdateCursor();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (captureOperation == CaptureOperation.DragInstrument)
            {
                if (ClientRectangle.Contains(e.X, e.Y))
                {
                    var buttonIdx = GetButtonAtCoord(e.X, e.Y, out var subButtonType);

                    var instrumentSrc = draggedInstrument;
                    var instrumentDst = buttonIdx >= 0 && buttons[buttonIdx].type == ButtonType.Instrument ? buttons[buttonIdx].instrument : null;

                    if (instrumentSrc != instrumentDst && instrumentSrc != null && instrumentDst != null && instrumentSrc.ExpansionType == instrumentDst.ExpansionType)
                    {
                        if (envelopeDragIdx == -1)
                        {
                            if (PlatformUtils.MessageBox($"Are you sure you want to replace all notes of instrument '{instrumentDst.Name}' with '{instrumentSrc.Name}'?", "Replace intrument", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                App.UndoRedoManager.BeginTransaction(TransactionScope.Project);
                                App.Project.ReplaceInstrument(instrumentDst, instrumentSrc);
                                App.UndoRedoManager.EndTransaction();

                                InstrumentReplaced?.Invoke(instrumentDst);
                            }
                        }
                        else
                        {
                            if (PlatformUtils.MessageBox($"Are you sure you want to copy the {Envelope.EnvelopeNames[envelopeDragIdx]} envelope of instrument '{instrumentSrc.Name}' to '{instrumentDst.Name}'?", "Copy Envelope", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                App.UndoRedoManager.BeginTransaction(TransactionScope.Instrument, instrumentDst.Id);
                                instrumentDst.Envelopes[envelopeDragIdx] = instrumentSrc.Envelopes[envelopeDragIdx].ShallowClone();
                                App.UndoRedoManager.EndTransaction();

                                InstrumentEdited?.Invoke(instrumentDst, envelopeDragIdx);
                                Invalidate();
                            }
                        }
                    }
                }
                else
                {
                    InstrumentDraggedOutside(draggedInstrument, PointToScreen(new Point(e.X, e.Y)));
                }
            }
            else if (captureOperation == CaptureOperation.MoveSlider)
            {
                App.UndoRedoManager.EndTransaction();
            }

            draggedInstrument = null;
            sliderDragButton = null;
            captureOperation = CaptureOperation.None;
            Capture = false;
        }

        private void StartCaptureOperation(MouseEventArgs e, CaptureOperation op)
        {
            Debug.Assert(captureOperation == CaptureOperation.None);
            captureMouseX = e.X;
            captureMouseY = e.Y;
            Capture = true;
            captureOperation = op;
            captureThresholdMet = !captureNeedsThreshold[(int)op];
        }

        private void AbortCaptureOperation()
        {
            Capture = false;
            captureOperation = CaptureOperation.None;

            if (App.UndoRedoManager.HasTransactionInProgress)
                App.UndoRedoManager.AbortTransaction();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            DoScroll(e.Delta > 0 ? buttonSizeY * 3 : -buttonSizeY * 3);
        }

        protected override void OnResize(EventArgs e)
        {
            UpdateRenderCoords();
            ClampScroll();
            base.OnResize(e);
        }

        bool UpdateSliderValue(Button button, MouseEventArgs e)
        {
            var buttonIdx = buttons.IndexOf(button);
            Debug.Assert(buttonIdx >= 0);

            bool shift = ModifierKeys.HasFlag(Keys.Shift);

            var actualWidth = Width - scrollBarSizeX;
            var buttonX = e.X;
            var buttonY = e.Y - buttonIdx * buttonSizeY;

            var paramVal = button.instrument.GetRealTimeParamValue(button.instrumentParam);
            var paramMin = Instrument.GetRealTimeParamMinValue(button.instrumentParam);
            var paramMax = Instrument.GetRealTimeParamMaxValue(button.instrumentParam);

            if (shift)
            {
                paramVal = Utils.Clamp(paramVal + (e.X - mouseLastX), paramMin, paramMax);
            }
            else
            {
                paramVal = (int)Math.Round(Utils.Lerp(paramMin, paramMax, Utils.Clamp((buttonX - (actualWidth - sliderPosX)) / (float)sliderSizeX, 0.0f, 1.0f)));
            }

            button.instrument.SetRealTimeParamValue(button.instrumentParam, paramVal);

            return (buttonX > (actualWidth - sliderPosX) &&
                    buttonX < (actualWidth - sliderPosX + sliderSizeX) &&
                    buttonY > (sliderPosY) &&
                    buttonY < (sliderPosY + sliderSizeY));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (captureOperation != CaptureOperation.None)
                return;

            bool left   = e.Button.HasFlag(MouseButtons.Left);
            bool middle = e.Button.HasFlag(MouseButtons.Middle) || (e.Button.HasFlag(MouseButtons.Left) && ModifierKeys.HasFlag(Keys.Alt));
            bool right  = e.Button.HasFlag(MouseButtons.Right);

            var buttonIdx = GetButtonAtCoord(e.X, e.Y, out var subButtonType);

            if (buttonIdx >= 0)
            {
                var button = buttons[buttonIdx];

                if (left)
                {
                    if (button.type == ButtonType.SongHeader)
                    {
                        if (subButtonType == SubButtonType.Add)
                        {
                            App.UndoRedoManager.BeginTransaction(TransactionScope.Project);
                            App.Project.CreateSong();
                            App.UndoRedoManager.EndTransaction();
                            RefreshButtons();
                            ConditionalInvalidate();
                        }
                    }
                    else if (button.type == ButtonType.Song)
                    {
                        if (button.song != selectedSong)
                        {
                            selectedSong = button.song;
                            SongSelected?.Invoke(selectedSong);
                            ConditionalInvalidate();
                        }
                    }
                    else if (button.type == ButtonType.InstrumentHeader)
                    {
                        if (subButtonType == SubButtonType.Add)
                        {
                            var instrumentType = Project.ExpansionNone;

                            if (App.Project.NeedsExpansionInstruments)
                            {
                                var expNames = new[] { Project.ExpansionNames[Project.ExpansionNone], App.Project.ExpansionAudioName };
                                var dlg = new PropertyDialog(PointToScreen(new Point(e.X, e.Y)), 260, true);
                                dlg.Properties.AddStringList("Expansion:", expNames, Project.ExpansionNames[Project.ExpansionNone] ); // 0
                                dlg.Properties.Build();

                                if (dlg.ShowDialog() == DialogResult.OK)
                                    instrumentType = dlg.Properties.GetPropertyValue<string>(0) == Project.ExpansionNames[Project.ExpansionNone] ? Project.ExpansionNone : App.Project.ExpansionAudio;
                                else
                                    return;
                            }

                            App.UndoRedoManager.BeginTransaction(TransactionScope.Project);
                            App.Project.CreateInstrument(instrumentType);
                            App.UndoRedoManager.EndTransaction();
                            RefreshButtons();
                            ConditionalInvalidate();
                        }
                        if (subButtonType == SubButtonType.LoadInstrument)
                        {
                            var filename = PlatformUtils.ShowOpenFileDialog("Open File", "Fami Tracker Instrument (*.fti)|*.fti");
                            if (filename != null)
                            {
                                App.UndoRedoManager.BeginTransaction(TransactionScope.Project);
                                var instrument = new FamitrackerInstrumentFile().CreateFromFile(App.Project, filename);
                                if (instrument == null)
                                    App.UndoRedoManager.AbortTransaction();
                                else
                                    App.UndoRedoManager.EndTransaction();
                            }

                            RefreshButtons();
                            ConditionalInvalidate();
                        }
                    }
                    else if (button.type == ButtonType.Instrument)
                    {
                        selectedInstrument = button.instrument;

                        if (selectedInstrument != null)
                        {
                            envelopeDragIdx = -1;
                            draggedInstrument = selectedInstrument;
                            StartCaptureOperation(e, CaptureOperation.DragInstrument);
                        }

                        if (subButtonType == SubButtonType.ExpandInstrument)
                        {                         
                            expandedInstrument = expandedInstrument == selectedInstrument ? null : selectedInstrument;
                            RefreshButtons();
                        }
                        if (subButtonType == SubButtonType.DPCM)
                        {
                            InstrumentEdited?.Invoke(selectedInstrument, Envelope.Count);
                        }
                        else if (subButtonType < SubButtonType.EnvelopeMax)
                        {
                            InstrumentEdited?.Invoke(selectedInstrument, (int)subButtonType);
                            envelopeDragIdx = (int)subButtonType;
                        }

                        InstrumentSelected?.Invoke(selectedInstrument);
                        ConditionalInvalidate();
                    }
                    else if (button.type == ButtonType.ParamSlider)
                    {
                        if (UpdateSliderValue(button, e))
                        {
                            App.UndoRedoManager.BeginTransaction(TransactionScope.Instrument, selectedInstrument.Id);

                            sliderDragButton = button;
                            StartCaptureOperation(e, CaptureOperation.MoveSlider);
                            ConditionalInvalidate();
                        }
                    }
                    else if (button.type == ButtonType.ParamCheckbox)
                    {
                        var actualWidth = Width - scrollBarSizeX;

                        if (e.X >= actualWidth - checkBoxPosX)
                        {
                            App.UndoRedoManager.BeginTransaction(TransactionScope.Instrument, selectedInstrument.Id);
                            var val = button.instrument.GetRealTimeParamValue(button.instrumentParam);
                            button.instrument.SetRealTimeParamValue(button.instrumentParam, val == 0 ? 1 : 0);
                            App.UndoRedoManager.EndTransaction();
                            ConditionalInvalidate();
                        }
                    }
                    else if (button.type == ButtonType.ParamList)
                    {
                        var actualWidth = Width - scrollBarSizeX;
                        var buttonX = e.X;
                        var leftButton  = buttonX > (actualWidth - sliderPosX) && buttonX < (actualWidth - sliderPosX + bmpButtonLeft.Size.Width);
                        var rightButton = buttonX > (actualWidth - sliderPosX + sliderSizeX - bmpButtonRight.Size.Width) && buttonX < (actualWidth - sliderPosX + sliderSizeX);
                        var delta = leftButton ? -1 : (rightButton ? 1 : 0);

                        if (leftButton || rightButton)
                        {
                            App.UndoRedoManager.BeginTransaction(TransactionScope.Instrument, selectedInstrument.Id);

                            var val = button.instrument.GetRealTimeParamValue(button.instrumentParam);

                            if (rightButton)
                                val = button.instrument.GetRealTimeParamNextValue(button.instrumentParam, val);
                            else
                                val = button.instrument.GetRealTimeParamPrevValue(button.instrumentParam, val);

                            button.instrument.SetRealTimeParamValue(button.instrumentParam, val);

                            App.UndoRedoManager.EndTransaction();
                            ConditionalInvalidate();
                        }
                    }
                }
                else if (right)
                {
                    if (button.type == ButtonType.Song && App.Project.Songs.Count > 1)
                    {
                        var song = button.song;
                        if (PlatformUtils.MessageBox($"Are you sure you want to delete '{song.Name}' ?", "Delete song", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            bool selectNewSong = song == selectedSong;
                            App.Stop();
                            App.UndoRedoManager.BeginTransaction(TransactionScope.Project);
                            App.Project.DeleteSong(song);
                            if (selectNewSong)
                                selectedSong = App.Project.Songs[0];
                            SongSelected?.Invoke(selectedSong);
                            App.UndoRedoManager.EndTransaction();
                            RefreshButtons();
                            ConditionalInvalidate();
                        }
                    }
                    else if (button.type == ButtonType.Instrument && button.instrument != null)
                    {
                        var instrument = button.instrument;

                        if (subButtonType < SubButtonType.EnvelopeMax)
                        {
                            App.UndoRedoManager.BeginTransaction(TransactionScope.Instrument, instrument.Id);
                            instrument.Envelopes[(int)subButtonType].Length = 0;
                            App.UndoRedoManager.EndTransaction();
                            ConditionalInvalidate();
                        }
                        else if (subButtonType == SubButtonType.Max)
                        {
                            if (PlatformUtils.MessageBox($"Are you sure you want to delete '{instrument.Name}' ? All notes using this instrument will be deleted.", "Delete intrument", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                bool selectNewInstrument = instrument == selectedInstrument;
                                App.StopEverything();
                                App.UndoRedoManager.BeginTransaction(TransactionScope.Project);
                                App.Project.DeleteInstrument(instrument);
                                if (selectNewInstrument)
                                    selectedInstrument = App.Project.Instruments.Count > 0 ? App.Project.Instruments[0] : null;
                                SongSelected?.Invoke(selectedSong);
                                InstrumentDeleted?.Invoke(instrument);
                                App.UndoRedoManager.EndTransaction();
                                RefreshButtons();
                                ConditionalInvalidate();
                            }
                        }
                    }
                }
            }

            if (middle)
            {
                mouseLastY = e.Y;
            }
        }

        private void EditProjectProperties(Point pt)
        {
            var project = App.Project;

            var dlg = new PropertyDialog(PointToScreen(pt), 320, true);
            dlg.Properties.AddString("Title :", project.Name, 31); // 0
            dlg.Properties.AddString("Author :", project.Author, 31); // 1
            dlg.Properties.AddString("Copyright :", project.Copyright, 31); // 2
            dlg.Properties.AddStringList("Expansion Audio :", Project.ExpansionNames, project.ExpansionAudioName, CommonTooltips.ExpansionAudio); // 3
            dlg.Properties.AddIntegerRange("Channels :", project.ExpansionNumChannels, 1, 8, CommonTooltips.ExpansionNumChannels); // 4 (Namco)
            dlg.Properties.AddStringList("Tempo Mode :", Project.TempoModeNames, Project.TempoModeNames[project.TempoMode], CommonTooltips.TempoMode); // 5
            dlg.Properties.SetPropertyEnabled(4, project.ExpansionAudio == Project.ExpansionN163);
            dlg.Properties.PropertyChanged += ProjectProperties_PropertyChanged;
            dlg.Properties.Build();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                App.UndoRedoManager.BeginTransaction(TransactionScope.Project);

                project.Name = dlg.Properties.GetPropertyValue<string>(0);
                project.Author = dlg.Properties.GetPropertyValue<string>(1);
                project.Copyright = dlg.Properties.GetPropertyValue<string>(2);

                var tempoMode = Array.IndexOf(Project.TempoModeNames, dlg.Properties.GetPropertyValue<string>(5));
                var expansion = Array.IndexOf(Project.ExpansionNames, dlg.Properties.GetPropertyValue<string>(3));
                var numChannels = dlg.Properties.GetPropertyValue<int>(4);

                var changedTempoMode   = tempoMode != project.TempoMode;
                var changedExpansion   = expansion != project.ExpansionAudio;
                var changedNumChannels = numChannels != project.ExpansionNumChannels;

                if (changedExpansion || changedNumChannels)
                {
                    if (project.ExpansionAudio == Project.ExpansionNone ||
                        (!changedExpansion && changedNumChannels) ||
                        PlatformUtils.MessageBox($"Switching expansion audio will delete all instruments and channels using the old expansion?", "Change expansion audio", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        App.StopEverything();
                        project.SetExpansionAudio(expansion, numChannels);
                        ProjectModified?.Invoke();
                        App.StartInstrumentPlayer();
                        Reset();
                    }
                }

                if (changedTempoMode)
                {
                    App.StopEverything();
                    if (tempoMode == Project.TempoFamiStudio)
                    {
                        if (!project.AreSongsEmpty)
                            PlatformUtils.MessageBox($"Converting from FamiTracker to FamiStudio tempo is extremely crude right now. It will ignore all speed changes and assume a tempo of 150. It is very likely that the songs will need a lot of manual corrections after.", "Change tempo mode", MessageBoxButtons.OK);
                        project.ConvertToFamiStudioTempo();
                    }
                    else if (tempoMode == Project.TempoFamiTracker)
                    {
                        if (!project.AreSongsEmpty)
                            PlatformUtils.MessageBox($"Converting from FamiStudio to FamiTracker tempo will simply set the speed to 1 and tempo to 150. It will not try to merge notes or do anything sophisticated.", "Change tempo mode", MessageBoxButtons.OK);
                        project.ConvertToFamiTrackerTempo(project.AreSongsEmpty);
                    }

                    ProjectModified?.Invoke();
                    App.StartInstrumentPlayer();
                    Reset();
                }

                App.UndoRedoManager.EndTransaction();
                ConditionalInvalidate();
            }
        }

        private void EditSongProperties(Point pt, Song song)
        {
            var dlg = new PropertyDialog(PointToScreen(pt), 220, true);

            dlg.Properties.UserData = song;
            dlg.Properties.AddColoredString(song.Name, song.Color); // 0
            dlg.Properties.AddColor(song.Color); // 1
            dlg.Properties.AddIntegerRange("Song Length :", song.Length, 1, Song.MaxLength, CommonTooltips.SongLength); // 2

            if (song.UsesFamiTrackerTempo)
            {
                dlg.Properties.AddIntegerRange("Tempo :", song.FamitrackerTempo, 32, 255, CommonTooltips.Tempo); // 3
                dlg.Properties.AddIntegerRange("Speed :", song.FamitrackerSpeed, 1, 31, CommonTooltips.Speed); // 4
                dlg.Properties.AddIntegerRange("Notes per Pattern :", song.PatternLength, 16, 256, CommonTooltips.NotesPerPattern); // 5
                dlg.Properties.AddIntegerRange("Notes per Bar :", song.BarLength, 2, 256, CommonTooltips.NotesPerBar); // 6
                dlg.Properties.AddLabel("BPM :", song.BPM.ToString(), CommonTooltips.BPM); // 7
            }
            else
            {
                dlg.Properties.AddIntegerRange("Frames per Note : ", song.NoteLength, 1, Song.MaxNoteLength, CommonTooltips.FramesPerNote); // 3
                dlg.Properties.AddIntegerRange("Notes per Pattern : ", song.PatternLength / song.NoteLength, 2, Pattern.MaxLength / song.NoteLength, CommonTooltips.NotesPerPattern); // 4
                dlg.Properties.AddIntegerRange("Notes per Bar : ", song.BarLength / song.NoteLength, 2, 256, CommonTooltips.NotesPerBar); // 5
                dlg.Properties.AddLabel("BPM :", song.BPM.ToString(), CommonTooltips.BPM); // 6

                if (!song.Project.UsesExpansionAudio)
                {
                    dlg.Properties.AddLabel("PAL Error :", $"{Song.ComputePalError(song.NoteLength):0.##} %", CommonTooltips.PalError); // 7
                    dlg.Properties.AddIntegerRange("PAL Skip Frame 1 : ", song.PalSkipFrames[0], -1, song.NoteLength - 1, CommonTooltips.PalSkipFrame); // 8
                    dlg.Properties.AddIntegerRange("PAL Skip Frame 2 : ", song.PalSkipFrames[1], -1, song.NoteLength - 1, CommonTooltips.PalSkipFrame); // 9
                    dlg.Properties.SetPropertyEnabled(8, Song.GetNumPalSkipFrames(song.NoteLength) >= 1);
                    dlg.Properties.SetPropertyEnabled(9, Song.GetNumPalSkipFrames(song.NoteLength) >= 2);
                    dlg.ValidateProperties += SongProperties_ValidateProperties;
                }
            }

            dlg.Properties.Build();
            dlg.Properties.PropertyChanged += SongProperties_PropertyChanged;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                App.UndoRedoManager.BeginTransaction(TransactionScope.Project);

                App.Stop();
                App.Seek(0);

                var newName = dlg.Properties.GetPropertyValue<string>(0);

                if (App.Project.RenameSong(song, newName))
                {
                    song.Color = dlg.Properties.GetPropertyValue<System.Drawing.Color>(1);

                    if (song.UsesFamiTrackerTempo)
                    {
                        song.FamitrackerTempo = dlg.Properties.GetPropertyValue<int>(3);
                        song.FamitrackerSpeed = dlg.Properties.GetPropertyValue<int>(4);
                        song.SetDefaultPatternLength(dlg.Properties.GetPropertyValue<int>(5));
                        song.SetBarLength(dlg.Properties.GetPropertyValue<int>(6));
                    }
                    else
                    {
                        var newNoteLength = dlg.Properties.GetPropertyValue<int>(3);

                        if (newNoteLength != song.NoteLength)
                        {
                            var convertTempo = PlatformUtils.MessageBox($"You changed the note length, do you want FamiStudio to attempt convert the tempo by resizing notes?", "Tempo Change", MessageBoxButtons.YesNo) == DialogResult.Yes;
                            song.ResizeNotes(newNoteLength, convertTempo);
                        }
                        
                        if (!song.Project.UsesExpansionAudio)
                        {
                            song.PalSkipFrames[0] = dlg.Properties.GetPropertyValue<int>(8);
                            song.PalSkipFrames[1] = dlg.Properties.GetPropertyValue<int>(9);
                        }
                        else
                        {
                            Song.GetDefaultPalSkipFrames(newNoteLength, song.PalSkipFrames);
                        }

                        song.SetDefaultPatternLength(dlg.Properties.GetPropertyValue<int>(4) * song.NoteLength);
                        song.SetBarLength(dlg.Properties.GetPropertyValue<int>(5) * song.NoteLength);
                    }

                    song.SetLength(dlg.Properties.GetPropertyValue<int>(2));
                    SongModified?.Invoke(song);
                    App.UndoRedoManager.EndTransaction();
                    RefreshButtons();
                }
                else
                {
                    App.UndoRedoManager.AbortTransaction();
                    SystemSounds.Beep.Play();
                }

                ConditionalInvalidate();
            }
        }

        private bool SongProperties_ValidateProperties(PropertyPage props)
        {
            var song = props.UserData as Song;
            var noteLength = props.GetPropertyValue<int>(3);
            var frame1 = props.GetPropertyValue<int>(8);
            var frame2 = props.GetPropertyValue<int>(9);
            var numPalSkipFrames = (frame1 >= 0 ? 1 : 0) + (frame2 >= 0 ? 1 : 0);
            var expectedCount = Song.GetNumPalSkipFrames(noteLength);

            if ((expectedCount > 0 && frame1 == frame2) || (numPalSkipFrames != expectedCount))
            {
                PlatformUtils.MessageBox($"PAL skip frames must be positive and different.", "PAL Skip Frames", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void SongProperties_PropertyChanged(PropertyPage props, int idx, object value)
        {
            var song = props.UserData as Song;

            if (selectedSong.UsesFamiTrackerTempo && (idx == 3 || idx == 4)) // 3/4 = Tempo/Speed
            {
                var tempo = props.GetPropertyValue<int>(3);
                var speed = props.GetPropertyValue<int>(4);

                props.SetLabelText(7, Song.ComputeFamiTrackerBPM(speed, tempo).ToString());
            }
            else if (idx == 3) // 3 = Note length
            {
                int noteLength = (int)value;

                props.UpdateIntegerRange(4, 1, Pattern.MaxLength / noteLength);
                props.SetLabelText(6, Song.ComputeFamiStudioBPM(noteLength).ToString());

                if (!song.Project.UsesExpansionAudio)
                {
                    var frames = new int[2];
                    Song.GetDefaultPalSkipFrames(noteLength, frames);

                    props.SetLabelText(7, $"{Song.ComputePalError(noteLength):0.##} %");
                    props.UpdateIntegerRange(8, frames[0], -1, noteLength - 1);
                    props.UpdateIntegerRange(9, frames[1], -1, noteLength - 1);
                    props.SetPropertyEnabled(8, Song.GetNumPalSkipFrames(noteLength) >= 1);
                    props.SetPropertyEnabled(9, Song.GetNumPalSkipFrames(noteLength) >= 2);
                }
            }
        }

        private void EditInstrumentProperties(Point pt, Instrument instrument)
        {
            var dlg = new PropertyDialog(PointToScreen(pt), 160, true, pt.Y > Height / 2);
            dlg.Properties.AddColoredString(instrument.Name, instrument.Color); // 0
            dlg.Properties.AddColor(instrument.Color); // 1
            if (instrument.IsEnvelopeActive(Envelope.Pitch))
                dlg.Properties.AddBoolean("Relative pitch:", instrument.Envelopes[Envelope.Pitch].Relative); // 2
            dlg.Properties.Build();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var newName = dlg.Properties.GetPropertyValue<string>(0);

                App.UndoRedoManager.BeginTransaction(TransactionScope.Project);

                if (App.Project.RenameInstrument(instrument, newName))
                {
                    instrument.Color = dlg.Properties.GetPropertyValue<System.Drawing.Color>(1);
                    if (instrument.IsEnvelopeActive(Envelope.Pitch))
                    {
                        var newRelative = dlg.Properties.GetPropertyValue<bool>(2);
                        if (instrument.Envelopes[Envelope.Pitch].Relative != newRelative)
                        {
                            if (!instrument.Envelopes[Envelope.Pitch].IsEmpty)
                            {
                                if (newRelative)
                                {
                                    if (PlatformUtils.MessageBox("Do you want to try to convert the pitch envelope from absolute to relative?", "Pitch Envelope", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        instrument.Envelopes[Envelope.Pitch].ConvertToRelative();
                                }
                                else
                                {
                                    if (PlatformUtils.MessageBox("Do you want to try to convert the pitch envelope from relative to absolute?", "Pitch Envelope", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        instrument.Envelopes[Envelope.Pitch].ConvertToAbsolute();
                                }
                            }

                            instrument.Envelopes[Envelope.Pitch].Relative = newRelative;
                        }
                    }
                    InstrumentColorChanged?.Invoke(instrument);
                    RefreshButtons();
                    ConditionalInvalidate();
                    App.UndoRedoManager.EndTransaction();
                }
                else
                {
                    App.UndoRedoManager.AbortTransaction();
                    SystemSounds.Beep.Play();
                }
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            var buttonIdx = GetButtonAtCoord(e.X, e.Y, out var subButtonType);

            if (buttonIdx >= 0)
            {
                if (captureOperation != CaptureOperation.None)
                    AbortCaptureOperation();

                var button = buttons[buttonIdx];
                var pt = new Point(e.X, e.Y);   

                if (button.type == ButtonType.ProjectSettings)
                {
                    EditProjectProperties(pt);
                }
                else if (button.type == ButtonType.Song)
                {
                    EditSongProperties(pt, button.song);
                }
                else if (button.type == ButtonType.Instrument && button.instrument != null && subButtonType == SubButtonType.Max)
                {
                    EditInstrumentProperties(pt, button.instrument);
                }
#if FAMISTUDIO_MACOS
                else
                {
                    // When pressing multiple times on mac, it creates click -> dbl click -> click -> dbl click sequences which
                    // makes the project explorer feel very sluggish. Interpret dbl click as clicks helps a lot.
                    OnMouseDown(e);
                }
#endif
            }
        }

        private void ProjectProperties_PropertyChanged(PropertyPage props, int idx, object value)
        {
            if (idx == 3)
            {
                props.SetPropertyEnabled(4, (string)value == Project.ExpansionNames[Project.ExpansionN163]);
            }
        }

        public void SerializeState(ProjectBuffer buffer)
        {
            buffer.Serialize(ref selectedSong);
            buffer.Serialize(ref selectedInstrument);
            buffer.Serialize(ref expandedInstrument);
            buffer.Serialize(ref scrollY);

            if (buffer.IsReading)
            {
                captureOperation = CaptureOperation.None;
                Capture = false;

                ClampScroll();
                RefreshButtons();
                ConditionalInvalidate();
            }
        }
    }
}
