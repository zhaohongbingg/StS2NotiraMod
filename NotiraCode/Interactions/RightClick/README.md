# Notira 右键事件系统

这是一个为 STS2 (Slay the Spire 2) mod 开发的右键事件系统，灵感来自 RitsuLib 的实现。

## 功能特性

- 支持为卡片、遗物、能力、药水等模型添加右键交互
- 支持优先级系统，可以控制右键处理的顺序
- 支持本地过滤和执行期判定
- 支持自定义右键处理器
- 支持通过接口实现或注册绑定的方式添加右键功能

## 文件结构

```
Interactions/RightClick/
├── IModRightClickHandler.cs          # 右键处理器接口
├── IModRightClickableModel.cs        # 可右键点击模型接口
├── ModRightClickBindingId.cs         # 右键绑定ID
├── ModRightClickContext.cs           # 本地右键上下文
├── ModRightClickExecutionContext.cs   # 执行时上下文
├── ModRightClickModelKind.cs         # 模型类型枚举
├── ModRightClickRegistry.cs          # 注册表和分发器
├── ModRightClickTrigger.cs           # 输入元数据
├── Patches/
│   └── ModRightClickCardPatch.cs     # 卡片右键事件补丁
└── Examples/
    └── RightClickExample.cs          # 使用示例
```

## 使用方法

### 方法一：实现 IModRightClickableModel 接口

在你的模型类上实现 `IModRightClickableModel` 接口（或其子接口）：

```csharp
public class MyCard : CardModel, IModRightClickableCard
{
    // 实现接口方法
    public bool CanHandleRightClickLocal(ModRightClickContext context)
    {
        return true; // 本地过滤
    }

    public bool CanExecuteRightClick(ModRightClickExecutionContext context)
    {
        return true; // 执行期判定
    }

    public async Task OnRightClick(ModRightClickExecutionContext context)
    {
        // 右键点击时执行的逻辑
        MainFile.Logger.Info("Card right-clicked!");
    }
}
```

### 方法二：注册右键绑定

在 mod 初始化时注册右键绑定：

```csharp
ModRightClickRegistry.Register<CardModel>(
    modId: "MyMod",
    localStem: "my_card_rightclick",
    canHandle: context => context.Model is CardModel card && card.Cost > 0,
    execute: async context =>
    {
        // 右键点击时执行的逻辑
        MainFile.Logger.Info("Card right-clicked via binding!");
    },
    priority: 0);
```

### 方法三：自定义右键处理器

创建一个实现 `IModRightClickHandler` 接口的处理器：

```csharp
public class MyRightClickHandler : IModRightClickHandler
{
    public int Priority => 100;

    public bool TryHandle(ModRightClickContext context)
    {
        // 处理右键点击
        MainFile.Logger.Info("Custom handler triggered!");
        return true; // 返回 true 以消耗输入
    }
}

// 注册处理器
ModRightClickRegistry.Register(new MyRightClickHandler());
```

## 支持的模型类型

- `IModRightClickableCard` - 卡片
- `IModRightClickableRelic` - 遗物
- `IModRightClickablePower` - 能力
- `IModRightClickablePotion` - 药水

## 注意事项

1. 右键事件系统会自动处理手牌中的卡片右键点击
2. 优先级越高，处理顺序越靠前
3. 本地过滤 (`CanHandleRightClickLocal`) 应该只使用稳定的本地 UI 信息
4. 执行期判定 (`CanExecuteRightClick`) 可以检查可变的游戏状态
5. 返回 `true` 会消耗输入，阻止其他处理器处理该事件

## 示例

查看 `Examples/RightClickExample.cs` 文件获取完整的使用示例。