# 右键事件系统测试指南

## 测试卡片

我已经创建了一个测试卡片 `TestRightClickCard`，用于验证右键事件系统是否正常工作。

### 卡片信息

- **名称**: 测试卡片 (Test Right Click Card)
- **类型**: 攻击卡 (Attack)
- **稀有度**: 基础 (Basic)
- **费用**: 1
- **效果**: 造成6点伤害
- **右键效果**: 获得1点能量

### 如何测试

1. **启动游戏**
   - 确保你的mod已正确加载
   - 开始一局新游戏

2. **获取测试卡片**
   - 测试卡片应该会自动出现在你的卡牌池中
   - 如果没有看到，可能需要通过控制台或其他方式添加

3. **测试右键功能**
   - 在战斗中，将鼠标悬停在测试卡片上
   - 右键点击该卡片
   - 观察是否获得1点能量
   - 检查控制台日志是否有以下输出：
     ```
     [TestCard] Right-clicked! Gaining 1 energy...
     [TestCard] Energy gained successfully!
     ```

### 预期行为

1. **右键点击时**:
   - 获得1点能量
   - 控制台显示日志信息
   - 卡片不会从手牌中移除

2. **左键点击时**:
   - 正常打出卡片
   - 造成6点伤害
   - 卡片从手牌移入弃牌堆

### 控制台日志

在游戏中打开控制台（通常是 `~` 键），你应该能看到类似以下的日志：

```
[Notira] Right-click system initialized
[RightClickTest] Test handler registered
[RightClickTest] Test binding registered
[RightClickTest] Test completed successfully!
[RightClickTest] Test binding disposed
```

当你右键点击测试卡片时，应该看到：

```
[TestCard] Right-clicked! Gaining 1 energy...
[TestCard] Energy gained successfully!
```

### 故障排除

如果右键功能不工作：

1. **检查控制台日志**
   - 确认右键系统已初始化
   - 查看是否有错误信息

2. **确认卡片存在**
   - 确保测试卡片在你的手牌中
   - 确保你在战斗中

3. **检查本地化**
   - 确保 `cards.json` 文件中有正确的本地化条目

4. **重新构建**
   - 运行 `dotnet build` 确保没有编译错误

### 代码结构

测试卡片的实现位于：
- `NotiraCode/Cards/Basic/TestRightClickCard.cs`
- `Notira/localization/zhs/cards.json` (本地化)

### 下一步

一旦右键功能验证成功，你可以：

1. **修改右键效果**
   - 编辑 `TestRightClickCard.cs` 中的 `OnRightClick` 方法
   - 实现你想要的任何效果

2. **为其他卡片添加右键功能**
   - 实现 `IModRightClickableCard` 接口
   - 或者使用 `ModRightClickRegistry.Register<TModel>()` 方法

3. **创建自定义处理器**
   - 实现 `IModRightClickHandler` 接口
   - 处理特定类型的右键点击

### 示例：修改右键效果

```csharp
public async Task OnRightClick(ModRightClickExecutionContext context)
{
    // 抽一张牌
    await PlayerCmd.DrawCards(1, context.Player);
    
    // 获得格挡
    await PlayerCmd.GainBlock(5, context.Player);
    
    // 施加力量
    await PowerCmd.Apply<StrengthPower>(context, ...);
}
```

### 支持

如果遇到问题，请检查：

1. 控制台日志中的错误信息
2. 确保所有文件都已正确保存
3. 确保项目已重新构建