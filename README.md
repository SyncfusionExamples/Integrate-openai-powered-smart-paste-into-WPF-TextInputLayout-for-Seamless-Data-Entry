# Integrate OpenAI-Powered Smart Paste into WPF-TextInputLayout for Seamless Data Entry

This repository contains a small WPF demo that implements an OpenAI-powered "Smart Paste" feature for a Syncfusion WPF `TextInputLayout`. The demo shows how pasted clipboard text can be sent to a model for parsing/normalization and then applied back into the UI as structured content.

Key goals:

- Demonstrate a focused implementation that integrates clipboard handling with an AI inference step.
- Show how to call a language model safely and asynchronously from a WPF app.
- Provide example transformation rules and a simple UI integration with `TextInputLayout` that can be adapted to real forms.

Contents

- `AISmartPasteDemo/` — the WPF solution and application sources (see `AISmartPasteDemo/AISmartPasteDemo.sln`).
- `AISmartPasteDemo/Service/` — contains `SemanticKernelService.cs`, which implements the model integration and request flow.
- `AISmartPasteDemo/ViewModel/` — contains `FeedbackViewModel.cs` (view-model logic used by the demo).
- `AISmartPasteDemo/Model/` — contains `FeedBackForm.cs` (the example data model).

Note: This repository does not include separate `docs/` or `examples/` folders. See the files listed above for the implementation details.

Installation & quick start

1. Clone the repository.
2. Open the solution in Visual Studio: [AISmartPasteDemo/AISmartPasteDemo.sln](AISmartPasteDemo/AISmartPasteDemo.sln) and run the project.

Alternatively, you can run the project from the command line (requires the .NET SDK):

```bash
dotnet run --project AISmartPasteDemo/AISmartPasteDemo.csproj
```

3. Configure your model API key or other credentials as implemented in `AISmartPasteDemo/Service/SemanticKernelService.cs` (the service code shows how the app reads configuration). Use an environment variable or a secure store appropriate for your environment.
4. Use the app's paste button or press Ctrl+V in the sample input field to trigger Smart Paste.

Behavior and safety

The demo performs client-side requests that send only clipboard text to the inference service, receives a structured response (JSON or plain text), and applies transformations locally. The implementation includes basic retry and simple rate-limiting places where you can add request sanitization, logging, or additional validation for production scenarios.

Extending the demo

- Add validation hooks to the `TextInputLayout` after the model returns a result.
- Tune or replace prompt templates and transformation logic in the service code to match your data shapes (addresses, CSV rows, SKUs, etc.).
- Replace or adapt `AISmartPasteDemo/Service/SemanticKernelService.cs` to use your preferred model client or SDK.
