# Unity编码规范

---

## 命名方式
1. 单词之间不以空格断开或连接号（-）、底线（_）连结。
2. 小驼峰命名法：第一个单词以小写字母开始，从第二个单词开始以后的每个单词的首字母都采用大写字母。
3. 大驼峰命名法：每个单词的首字母都采用大写字母。

## 命名规则
### 命名空间
1. 命名方式：大驼峰命名法
2. 示例：`namespace System.Security { ... }`

### 类
1. 命名方式：大驼峰命名法
2. 示例：`public class StreamReader { ... }`

### 接口
1. 命名方式：大驼峰命名法
2. 示例：`public interface IEnumerable { ... }`

### 枚举
1. 命名方式：大驼峰命名法
2. 示例：`public enum FileMode { Append, ... }`

### 方法
1. 命名方式：大驼峰命名法
2. 示例：`public virtual string ToString();`

### 常量
1. 命名方式：大驼峰命名法
2. 示例：`private double Pi=3.1415926;`

### 属性
1. 命名方式：大驼峰命名法
2. 示例：`public int Length { get; }`

### 字段
1. 命名方式：小驼峰命名法 + "m"前缀
2. 示例：`private int mLength;`

### 参数
1. 命名方式：小驼峰命名法
2. 示例：`public int ToInt32(string value); `

### 临时变量
1. 命名方式：小驼峰命名法
2. 示例：`int temp;`

### 变量后缀
|类型|后缀|示例|
|----|----|----|
|GameObject|Go|`GameObject mDialogGo;`
|Transfom  |Trans|`Transform mScrollViewTrans;`
|RectTransform|RTrans|`RectTransform mScrollContentRTrans;`
|Array|复数形式|`int[] mMembers;`
|List|List|`List<int> mMemberList`
|Dictionary|Dict|`Dictionary<int,string> mIDForNameDict;`
|Action|Act|`Action mReleaseAct;`
|Func|Func|`Func mReleaseFunc`


## 参考
1. [名称准则](https://docs.microsoft.com/zh-cn/previous-versions/dotnet/netframework-2.0/ms229002(v=vs.80))
2. [框架设计准则](https://docs.microsoft.com/zh-cn/dotnet/standard/design-guidelines/)
3. [Internal Coding Guidelines](https://blogs.msdn.microsoft.com/brada/2005/01/26/internal-coding-guidelines/)
4. [C# Coding Standards and Naming Conventions](https://www.dofactory.com/reference/csharp-coding-standards)