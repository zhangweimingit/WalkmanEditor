using HandyControl.Tools.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalkmanEditor.Util.SpeechSynthesis
{
    public class VoiceSupported
    {
        //        [TypeConverter(typeof(EnumDescriptionTypeConverter))]
        //        public enum EditStepsEnum
        //        {
        //            [Description("输入英文文本")]
        //            aa = "zh-CN-XiaoxiaoNeural",
        ////            zh-CN-XiaoxiaoNeural（女）
        ////zh-CN-YunxiNeural（男）
        ////zh-CN-YunjianNeural（男）
        ////zh-CN-XiaoyiNeural（女）
        ////zh-CN-YunyangNeural（男）
        ////zh-CN-XiaochenNeural（女）
        ////zh-CN-XiaohanNeural（女）
        ////zh-CN-XiaomengNeural（女）
        ////zh-CN-XiaomoNeural（女）
        ////zh-CN-XiaoqiuNeural（女）
        ////zh-CN-XiaoruiNeural（女）
        ////zh-CN-XiaoshuangNeural（女性、儿童）
        ////zh-CN-XiaoyanNeural（女）
        ////zh-CN-XiaoyouNeural（女性、儿童）
        ////zh-CN-XiaozhenNeural（女）
        ////zh-CN-YunfengNeural（男）
        ////zh-CN-YunhaoNeural（男）
        ////zh-CN-YunxiaNeural（男）
        ////zh-CN-YunyeNeural（男）
        ////zh-CN-YunzeNeural（男）
        ////zh-CN-XiaochenMultilingualNeural1,3（女）
        ////zh-CN-XiaorouNeural1（女）
        ////zh-CN-XiaoxiaoDialectsNeural1（女）
        ////zh-CN-XiaoxiaoMultilingualNeural1,3（女）
        ////zh-CN-XiaoyuMultilingualNeural1,3（女）
        ////zh-CN-YunjieNeural1（男）
        ////zh-CN-YunyiMultilingualNeural1,3（男）
        //        }
        public enum MyEnum
        {
            [Description("Value1")] // 添加自定义属性
            Value1 = "String1",

            [Description("Value2")]
            Value2 = "String2"
        }
    }
}
