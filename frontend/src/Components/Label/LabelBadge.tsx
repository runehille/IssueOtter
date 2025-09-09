import { Label } from "../../Models/Issue";

type Props = {
  label: Label;
  size?: "sm" | "md" | "lg";
  removable?: boolean;
  onRemove?: (labelId: number) => void;
};

const LabelBadge = ({ label, size = "sm", removable = false, onRemove }: Props) => {
  const sizeClasses = {
    sm: "badge-sm",
    md: "badge-md", 
    lg: "badge-lg"
  };

  const handleRemove = (e: React.MouseEvent) => {
    e.stopPropagation();
    if (onRemove) {
      onRemove(label.id);
    }
  };

  return (
    <div 
      className={`badge ${sizeClasses[size]} text-white font-medium`}
      style={{ backgroundColor: label.color }}
      title={label.description || label.name}
    >
      <span>{label.name}</span>
      {removable && (
        <button 
          className="ml-1 text-white hover:text-gray-200"
          onClick={handleRemove}
          aria-label={`Remove ${label.name} label`}
        >
          ×
        </button>
      )}
    </div>
  );
};

export default LabelBadge;