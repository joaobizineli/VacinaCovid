import { Card, CardContent } from "../ui/card";

interface StatCardProps {
  label: string;
  value: number | string;
  tag: string;
  tagClassName?: string;
}

export function StatCard({ label, value, tag, tagClassName }: StatCardProps) {
  return (
    <Card className="border-[0.5px] border-[#e0e4dd] shadow-none rounded-[10px]">
      <CardContent className="p-[14px_16px]">
        <div className="text-[12px] text-[#999] mb-[5px]">{label}</div>
        <div className="text-[22px] font-medium text-[#575E55] leading-none mb-[6px]">{value}</div>
        <span className={`text-[11px] px-[8px] py-[2px] rounded-full inline-block ${tagClassName}`}>
          {tag}
        </span>
      </CardContent>
    </Card>
  );
}