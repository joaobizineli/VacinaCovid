import { Card, CardContent, CardHeader } from "@/components/ui/card";
import type { ReactNode } from "react";

interface BaseCardProps {
  title: string;
  subtitle?: string;
  children: ReactNode;
  className?: string;
}

export function BaseCard({ title, subtitle, children, className }: BaseCardProps) {
  return (
    <Card className={`border-[0.5px] border-[#e0e4dd] shadow-none rounded-[10px] ${className ?? ""}`}>
      <CardHeader className="px-4 py-[13px] border-b border-[#e0e4dd] flex-row items-center justify-between space-y-0">
        <span className="text-[15px] font-semibold text-[#575E55]">{title}</span>
        {subtitle && <span className="text-[13px] text-[#aaa] font-normal">{subtitle}</span>}
      </CardHeader>
      <CardContent className="p-3">
        {children}
      </CardContent>
    </Card>
  );
}