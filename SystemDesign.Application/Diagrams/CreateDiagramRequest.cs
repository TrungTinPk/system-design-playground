namespace SystemDesign.Application.Diagrams;

public sealed record CreateDiagramRequest(
    string Name,
    string Content);
